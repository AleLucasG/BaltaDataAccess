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
        
            using (var connection = new SqlConnection(connectionString))
            {  
                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] ORDER BY [Title]");

                foreach(var category in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }   
          
        }
    }
}


