using System.Threading;
using System.Threading.Tasks;
using FinBot.Domain.Models.Entities;

namespace FinBot.Domain.Interfaces
{
    public interface IBaseRepositoryDb<T> : IRepositoryReader<T> where T : Entity
    {
        Task<bool> Create(T entity, CancellationToken cancel = default);
        Task<T> Update(T entity, CancellationToken cancel = default);
        Task<bool> Delete(int id, CancellationToken cancel = default);
    }
}
