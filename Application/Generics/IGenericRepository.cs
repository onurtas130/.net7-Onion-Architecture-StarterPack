using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Application.Generics
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TOut>> GetAllAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true);

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true);

        Task<TEntity> GetByIdAsync(object id, bool tracking = true);

        Task<TOut> GetByIdAsync<TOut>(object id, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true);

        Task<TOut> GetAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entities);

        void Remove(TEntity entity);

        Task RemoveByIdAsync(object id);

        void Update(TEntity entity);

        void UpdateRange(List<TEntity> entities);
    }
}
