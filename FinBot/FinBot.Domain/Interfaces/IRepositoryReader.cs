using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinBot.Domain.Models.Entities;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryReader<T> where T : Entity, new()
    {
        Task<bool> ExistEntity(int id, CancellationToken cancel = default);
        Task<IEnumerable<T>> GetCollection(CancellationToken cancel = default);
        Task<T> GetEntity(T entity, CancellationToken cancel = default);
        Task<T> GetEntityId(int id, CancellationToken cancel = default);
    }
}
