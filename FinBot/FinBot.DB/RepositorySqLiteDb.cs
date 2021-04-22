using System;
using System.Collections.Generic;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;

namespace FinBot.DB
{
    public class RepositorySqLiteDb : IRepositoryDb
    {
        public void Create(Category category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Category category)
        {
            throw new NotImplementedException();
        }


        public List<Category> GetCategoryView(CategoryView view)
        {
            throw new NotImplementedException();
        }
    }
}
