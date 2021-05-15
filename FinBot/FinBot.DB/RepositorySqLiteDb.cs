using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using Microsoft.Data.Sqlite;

namespace FinBot.DB
{
    public class RepositorySqLiteDb : IBaseRepositoryDb
    {
        private const string CONNECTIONSTRING = "Data Source=MyFin.db;Cache=Shared;";


        /// <summary>
        /// Метод создание сущности Category.
        /// </summary>
        /// <param name="nameCategory">Название категории.</param>
        /// <param name="role">true = income категория, false = consumption категория</param>
        /// <returns></returns>
        public bool CreateCategory(string nameCategory, bool role)
        {
            int intRole;
            intRole = role ? 1 : 0;
            var category = new Category() { Name = nameCategory, Role = role };

            using (var db = new SqliteConnection(CONNECTIONSTRING))
            {
                db.Execute("INSERT INTO Categories (Name, Role) VALUES (@Name, @Role)", category);
                return true;
            }
        }

        /// <summary>
        /// Метод удаления категории.
        /// </summary>
        /// <param name="id">Id категории</param>
        /// <returns>true - объект удален, false - ошибка удаления.</returns>
        public bool DeleteCategory(int id)
        {
            using (var db = new SqliteConnection(CONNECTIONSTRING))
            {
                db.Execute("DELETE FROM Categories WHERE Id = @id", new { id });
                return true;
            }
        }

        /// <summary>
        /// Метод 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }


        public bool CreateIncome(Income income)
        {
            using (var db = new SqliteConnection(CONNECTIONSTRING))
            {
                db.Execute(
                    "INSERT INTO Incomes (Date, Money, CategoryId) VALUES (@Date, @Money, @CategoryId)",
                    income);
                return true;
            }
        }

        public bool DeleteIncome(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditIncome(Income income)
        {
            throw new NotImplementedException();
        }

        public List<Income> GetCollectionIncomes()
        {
            var sqlExpression = "SELECT * FROM Incomes";
            using (IDbConnection dbConnection = new SqlConnection(CONNECTIONSTRING))
            {
                return dbConnection.Query<Income>(sqlExpression).ToList();
            }
        }

        public bool CreateConsumption(Consumption consumption)
        {

            using (var db = new SqliteConnection(CONNECTIONSTRING))
            {
                db.Execute(
                    "INSERT INTO Consumptions (Date, Money, CategoryId) VALUES (@Date, @Money, @CategoryId)",
                    consumption);
                return true;
            }
        }

        public bool DeleteConsumption(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditConsumption(Consumption consumption)
        {
            throw new NotImplementedException();
        }

        public List<Consumption> GetCollectionConsumptions()
        {
            var sqlExpression = "SELECT * FROM Consumptions";
            using (IDbConnection dbConnection = new SqliteConnection(CONNECTIONSTRING))
            {
                return dbConnection.Query<Consumption>(sqlExpression).ToList();
            }
        }


        public List<Category> GetCollectionCategories()
        {
            var sqlExpression = "SELECT * FROM Categories";
            using (IDbConnection dbConnection = new SqliteConnection(CONNECTIONSTRING))
            {
                return dbConnection.Query<Category>(sqlExpression).ToList();
            }

        }

        public Category GetCategory(string name)
        {
            using (var db = new SqliteConnection(CONNECTIONSTRING))
            {
                return db.QueryFirstOrDefault<Category>("SELECT * FROM Categories WHERE Name = @name", new { name });
            }
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
