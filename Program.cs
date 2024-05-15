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
            category.Featured = false;

            // nunca concatenar strings
            // criação de parametros definidos com @ na querie
            var insertSql = @"INSERT INTO 
                                [Category] 
                             VALUES(
                                @Id, 
                                @Title, 
                                @Url, 
                                @Summary, 
                                @Order, 
                                @Description, 
                                @Featured)";

            using (var connection = new SqlConnection(connectionString))
            {  
                // vai executar o Insert e mapear os parametros
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });

                Console.WriteLine($"{rows} linhas inseridas.");

                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] ORDER BY [Title]");
                foreach(var item in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }   
          
        }
    }
}


