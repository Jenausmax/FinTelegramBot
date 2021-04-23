using System;
using System.Collections.Generic;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using Microsoft.Data.Sqlite;

namespace FinBot.DB
{
    public class RepositorySqLiteDb : IRepositoryDb
    {
        private const string CONNECTIONSTRING = "Data Source=MyFin.db;Cache=Shared;";


        /// <summary>
        /// Создание сущности Category.
        /// </summary>
        /// <param name="nameCategory">Название категории.</param>
        /// <param name="role">true = income категория, false = consumption категория</param>
        /// <returns></returns>
        public bool CreateCategory(string nameCategory, bool role)
        {
            int intRole;
            if (role)
            {
                intRole = 1;
            }
            else
            {
                intRole = 0;
            }
            var category = new Category() { Name = nameCategory, Role = role };
            using (var connection = new SqliteConnection(CONNECTIONSTRING))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Categories (Name, Role) VALUES ('{category.Name}', {intRole})";
                int number = command.ExecuteNonQuery();



                connection.Close();
                return true;
            }
        }



        public bool CreateIncome(int idCategory, Income income)
        {
            if (income != null)
            {
                using (var connection = new SqliteConnection(CONNECTIONSTRING))
                {
                    connection.Open();

                    var command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Income (Date, Money, CategoryId) VALUES ('{income.Date}',{income.Money},{idCategory})";

                    connection.Close();
                    return true;
                }
            }

            return false;
        }

        public bool CreateConsumption(int idCategory, Consumption consumption)
        {
            if (consumption != null)
            {
                using (var connection = new SqliteConnection(CONNECTIONSTRING))
                {
                    connection.Open();

                    var command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Income (Date, Money, CategoryId) VALUES ('{consumption.Date}',{consumption.Money},{idCategory})";

                    connection.Close();
                    return true;
                }
            }

            return false;
        }

        public bool Delete<T>(T model, int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit<T>(T model)
        {
            throw new NotImplementedException();
        }




        public List<Category> GetCollectionCategories()
        {
            var categories = new List<Category>();
            string sqlExpression = "SELECT * FROM Categories";
            using (var connection = new SqliteConnection(CONNECTIONSTRING))
            {
                connection.Open();
                var command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int roleValue = reader.GetInt32(2);
                            bool role;
                            if (roleValue == 0)
                            {
                                role = false;
                            }
                            else
                            {
                                role = true;
                            }

                            var category = new Category()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Role = role
                            };
                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }




        public Dictionary<Category, Consumption> GetCollectionOneCategoryConsumptions(Category category)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Category, Income> GetCollectionOneCategoryIncome(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
