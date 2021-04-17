using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Models;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDb
    {
        void Create(Category category);
        void Delete(int id);
        void Edit(int id);
        Category GetCategory();
    }
}
