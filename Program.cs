using System;
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";
        
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Feature = false;

            var insertSql = @"INSERT INTO 
                                [Category] 
                             VALUES(NEWID(), id, title, url, summary, order, description, featured)";

            using (var connection = new SqlConnection(connectionString))
            {  
                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] ORDER BY [Title]");
                foreach(var item in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }   
          
        }
    }
}


