using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.EF.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _db;

        public Repository(DbContext context)
        {
            _context = context;
            _db = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null) return false;

            _db.Remove(entity);
            return true;
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            TEntity? entity = await GetByIdAsync(id);
            if (entity == null)
                return false;
            await DeleteAsync(entity);
            return true;
        }

        public async Task<List<TEntity>?> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null) return false;

            _db.Update(entity);
            return true;
        }
    }
}
