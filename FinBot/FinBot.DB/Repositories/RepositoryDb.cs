using FinBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using FinBot.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
            return await Set.ToArrayAsync();
        }

        public async Task<T> GetEntity(T entity, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetEntityId(int id, CancellationToken cancel = default)
        {
            return await Set.FirstOrDefaultAsync<T>(i => i.Id == id, cancel);
        }

        public async Task<bool> Create(T entity, CancellationToken cancel = default)
        {
            await Set.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update(T entity, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
