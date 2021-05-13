using FinBot.Domain.Models;
using System.Collections.Generic;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDbConsumption
    {
        bool CreateConsumption(Consumption consumption);
        bool DeleteConsumption(int id);
        bool EditConsumption(Consumption consumption);
        List<Consumption> GetCollectionConsumptions();
        Dictionary<Category, Consumption> GetCollectionOneCategoryConsumptions(Category category);
    }
}
