using System.Collections.Generic;
using FinBot.Domain.Models;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDb
    {
        void Create(Category category);
        void Delete(int id);
        void Edit(Category category);
        List<Category> GetCategoryView(CategoryView view);
    }
}
