using Dapper;
using ImagesURLValidator.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ImagesURLValidator.Repository
{
    public class Repository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["OutputDB"].ConnectionString;
        public Repository() { } 
        public int Insert(IList<OutputModel> outputModels)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string insertStatement = "INSERT INTO dbo.Output VALUES (@URI_Part_Number, @URL_Text, @URL_Correct)";
                    int result = connection.Execute(insertStatement, outputModels);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
