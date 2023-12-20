using Domain.Entities.Common;
using Domain.GeneralModels;
using System.Linq.Expressions;


namespace Application.Generics
{
    public interface IGenericService<TEntity> where TEntity : BaseEntity
    {
        Task<ServiceResultExt<IEnumerable<TOut>>> GetAllAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<ServiceResultExt<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true);

        Task<ServiceResultExt<IEnumerable<TOut>>> GetAllAsync<TOut>(Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<ServiceResultExt<IEnumerable<TEntity>>> GetAllAsync(bool tracking = true);

        Task<ServiceResultExt<TEntity>> GetByIdAsync(object id, bool tracking = true);

        Task<ServiceResultExt<TOut>> GetByIdAsync<TOut>(object id, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<ServiceResultExt<TEntity>> GetAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true);

        Task<ServiceResultExt<TOut>> GetAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true);

        Task<ServiceResult> AddAsync(TEntity entity);

        Task<ServiceResult> AddRangeAsync(List<TEntity> entities);

        Task<ServiceResult> RemoveAsync(TEntity entity);

        Task<ServiceResult> RemoveByIdAsync(object id);

        Task<ServiceResult> UpdateAsync(TEntity entity);

        Task<ServiceResult> UpdateRangeAsync(List<TEntity> entities);
    }
}
