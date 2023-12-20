using Application.Generics;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Generics
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;

            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            foreach (var item in entities)            
                item.CreatedDate = DateTime.UtcNow;

            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().ToListAsync();

            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(Expression<Func<TEntity,TOut>> @select, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().Select(@select).ToListAsync();

            return await _dbSet.Select(@select).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().Where(@where).ToListAsync();

            return await _dbSet.Where(@where).ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().Where(@where).Select(@select).ToListAsync();

            return await _dbSet.Where(@where).Select(@select).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());

            return await _dbSet.FindAsync(id);
        }

        public async Task<TOut> GetByIdAsync<TOut>(object id, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().Where(e => e.Id.ToString() == id.ToString()).Select(@select).FirstOrDefaultAsync();

            return await _dbSet.Where(e => e.Id.ToString() == id.ToString()).Select(@select).FirstOrDefaultAsync(); 
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(@where);

            return await _dbSet.FirstOrDefaultAsync(@where);
        }

        public async Task<TOut> GetAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            if (!tracking)
                return await _dbSet.AsNoTracking().Where(@where).Select(@select).FirstOrDefaultAsync();

            return await _dbSet.Where(@where).Select(@select).FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public async Task RemoveByIdAsync(object id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());

            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            _dbSet.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            foreach (var item in entities)
                item.UpdatedDate = DateTime.UtcNow;

            _dbSet.UpdateRange(entities);
        }

    }
}
