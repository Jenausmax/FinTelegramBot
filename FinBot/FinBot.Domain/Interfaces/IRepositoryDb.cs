using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FinBot.Domain.Models;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDb
    {
        bool CreateCategory(string nameCategory, bool role);
        bool CreateIncome(int idCategory, Income income);
        bool CreateConsumption(int idCategory, Consumption consumption);

        bool Delete<T>(T model, int id);
        bool Edit<T>(T model);
        List<Category> GetCollectionCategories();
        Dictionary<Category, Income> GetCollectionOneCategoryIncome(Category category);
        Dictionary<Category, Consumption> GetCollectionOneCategoryConsumptions(Category category);

    }
}
