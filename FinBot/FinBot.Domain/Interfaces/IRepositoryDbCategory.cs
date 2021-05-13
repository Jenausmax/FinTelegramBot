using FinBot.Domain.Models;
using System.Collections.Generic;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDbCategory
    {
        bool CreateCategory(string nameCategory, bool role);
        bool DeleteCategory(int id);
        bool EditCategory(Category category);
        List<Category> GetCollectionCategories();
        Category GetCategory(string name);
    }
}
