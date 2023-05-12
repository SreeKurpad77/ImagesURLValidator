using ExcelReader.WorkBookProcessor.ImagesWorkBook;
using ImagesURLValidator.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImagesURLValidator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFilePath = ConfigurationManager.AppSettings["SourceFilePath"];
            IList<OutputModel> outputModels = new List<OutputModel>();
            try
            {
                Console.WriteLine($"Started processing file {sourceFilePath}");
                using (FileStream excelFile = File.OpenRead(sourceFilePath))
                {
                    ImagesWorkBookProcessor imagesWorkBookProcessor = new ImagesWorkBookProcessor();
                    var result = imagesWorkBookProcessor.ProcessWorkbookData(excelFile);
                    Console.WriteLine($"Count of URLS in the excel file: {result.Count}");
                    Console.WriteLine($"Validating URLs...");
                    using (var progress = new ProgressBar())
                    {
                        Parallel.ForEach(result, imageURL =>
                        {
                            OutputModel output = new OutputModel(imageURL as InputModel);
                            var imageUrlQuery = ValidateImageURLAsync(imageURL as InputModel).Result;
                            if (imageUrlQuery.HasValue)
                                output.URL_Correct = imageUrlQuery.Value ? "Y" : "N";
                            else
                                output.URL_Correct = "Error";
                            lock (outputModels)
                            {
                                outputModels.Add(output);
                                progress.Report((double) outputModels.Count/result.Count);
                            }
                        }
                        );
                    }

                }

                Console.WriteLine($"URLS Validated Count : {outputModels.Count}");

                if (outputModels.Count > 0)
                {
                    Repository.Repository repository = new Repository.Repository();
                    var result = repository.Insert(outputModels);
                    Console.WriteLine($"Results saved in DB : {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("End of process");
            Console.ReadLine();
        }
        static async Task<bool?> ValidateImageURLAsync(InputModel inputModel)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var result = await httpClient.GetAsync(inputModel.URL_Text);
                    if (result.IsSuccessStatusCode)
                        return true;
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return false;
                    else
                        return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
