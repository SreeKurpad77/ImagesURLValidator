using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace ExcelReader.WorkbookProcessor.Base
{
    public class WorksheetSettingsHelper
    {
        public static string ConfigPath = ConfigurationManager.AppSettings["WorksheetSettingsFilePath"];
        public static WorkBookConfig GetWorkBookSettings()
        {
            StreamReader streamReader = File.OpenText(ConfigPath);
            string json = streamReader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<WorkBookConfig>(json);
            return result;
        }
    }
}
