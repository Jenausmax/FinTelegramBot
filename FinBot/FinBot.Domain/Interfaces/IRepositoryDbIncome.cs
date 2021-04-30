using FinBot.Domain.Models;
using System.Collections.Generic;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDbIncome
    {
        bool CreateIncome(int idCategory, Income income);
        bool DeleteIncome(int id);
        bool EditIncome(Income income);
        List<Income> GetCollectionIncomes();
        Dictionary<Category, Income> GetCollectionOneCategoryIncome(Category category);
    }
}
