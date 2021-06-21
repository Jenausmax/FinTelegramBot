using FinBot.Domain.Interfaces;
using FinBot.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinBot.DB.Repositories
{
    public class RepositoryDb<T> : IBaseRepositoryDb<T> where T : Entity
    {
        private readonly DataDb _db;
        protected DbSet<T> Set { get; }
        public RepositoryDb(DataDb db)
        {
            _db = db;
            Set = _db.Set<T>();

        }

        public async Task<bool> ExistEntity(int id, CancellationToken cancel = default)
        {
            return await Set.AnyAsync(i => i.Id == id, cancel);
        }

        public async Task<IEnumerable<T>> GetCollection(CancellationToken cancel = default)
        {
            return await Set.ToArrayAsync(cancel);
        }

        public async Task<T> GetEntity(T entity, CancellationToken cancel = default)
        {
            return await GetEntityId(entity.Id, cancel);
        }

        public async Task<T> GetEntityId(int id, CancellationToken cancel = default)
        {
            return await Set.FirstOrDefaultAsync<T>(i => i.Id == id, cancel);
        }

        public async Task<bool> Create(T entity, CancellationToken cancel = default)
        {
            await Set.AddAsync(entity, cancel);
            await _db.SaveChangesAsync(cancel);
            return true;
        }

        public async Task<T> Update(T entity, CancellationToken cancel = default)
        {
            var entityModify = Set.Update(entity);
            await _db.SaveChangesAsync(cancel);
            return entityModify as T;
        }

        public async Task<bool> Delete(int id, CancellationToken cancel = default)
        {
            var entity = await GetEntityId(id, cancel);
            if (entity == null) return false;

            Set.Remove(entity);
            await _db.SaveChangesAsync(cancel);

            return true;
        }
    }
}
