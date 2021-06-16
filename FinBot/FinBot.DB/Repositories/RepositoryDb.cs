using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;

namespace FinBot.DB.Repositories
{
    public class RepositoryDb<T> : IBaseRepositoryDb<T> where T : IEntity
    {
        public Task<bool> ExistEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetCollection()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
