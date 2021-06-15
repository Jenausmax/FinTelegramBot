using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryReader<T> where T : IEntity
    {
        Task<bool> ExistEntity(int id);
        Task<IEnumerable<T>> GetCollection();
        Task<T> GetEntity(T entity);
        Task<T> GetEntityId(int id);
    }
}
