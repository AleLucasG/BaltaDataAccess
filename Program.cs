﻿using System;
using System.Data;
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
                //CreateCategory(connection);
                //CreateManyCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);
                //ListCategories(connection);
                //CreateCategory(connection);
                //GetCategory(connection);
                //ExecuteProcedure(connection);
                ExecuterReadProcedure(connection);
            }  
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach(var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }

        }

        static void GetCategory(SqlConnection connection)
        {
            var category = connection.QueryFirstOrDefault<Category>("SELECT TOP 1 [Id], [Title] FROM [Category] WHERE [Id]=@id",
                new
                {
                    id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
                });
            Console.WriteLine($"{category.Id} - {category.Title}");
        }

        static void CreateCategory(SqlConnection connection)
        {
            // inserir varios intens ao mesmo tempo
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
            Console.WriteLine($"{rows} linhas inseridas");

        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                //  struct Guid representa um identificador único global
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });
            Console.WriteLine($"{rows} registros atualizados");
        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
            });

            Console.WriteLine($"{rows} registros excluídos");
        }


         static void CreateManyCategory(SqlConnection connection)
        {
            // inserir varios intens ao mesmo tempo
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            // inserir varios intens ao mesmo tempo
            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

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

            // vai executar o Insert e mapear os parametros
                var rows = connection.Execute(insertSql, new[]
                {
                    new
                    {
                        category.Id,
                        category.Title,
                        category.Url,
                        category.Summary,
                        category.Order,
                        category.Description,
                        category.Featured
                    },
                     new
                    {
                        category2.Id,
                        category2.Title,
                        category2.Url,
                        category2.Summary,
                        category2.Order,
                        category2.Description,
                        category2.Featured
                    }
                    
                });
                Console.WriteLine($"{rows} linhas inseridas.");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "spDeleteStudent";
            var pars = new { StudentId = "56211af5-5dfa-4137-ae3c-0bd490daa21f" };
            var affectRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{affectRows} linhas afetadas");
        }

        static void ExecuterReadProcedure(SqlConnection connection)
        {
            // vai retornar uma lista de cursos
            var procedure = "spGetCoursesByCategory";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

            foreach(var item in courses)
            {
                Console.WriteLine(item.Id);
            }
        }

    }    
}

