using Application.Generics;
using Domain.Entities.Common;
using Domain.GeneralModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Generics
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repo;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.Repository<TEntity>();
        }

        public async Task<ServiceResult> AddAsync(TEntity entity)
        {
            await _repo.AddAsync(entity);
            var number = await _unitOfWork.SaveChangesAsync();

            if(number > 0)
                return new ServiceResult(explanation: "data is added");

            return new ServiceResult(explanation: "no data is added");
        }

        public async Task<ServiceResult> AddRangeAsync(List<TEntity> entities)
        {
            await _repo.AddRangeAsync(entities);
            var number = await _unitOfWork.SaveChangesAsync();

            if (number > 0)
                return new ServiceResult(explanation: "datas are added");

            return new ServiceResult(explanation: "no data is added");
        }

        public async Task<ServiceResultExt<IEnumerable<TOut>>> GetAllAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            var resultData = await _repo.GetAllAsync(@where, @select, tracking);

            return new ServiceResultExt<IEnumerable<TOut>>(resultObject: resultData, totalCount: resultData?.Count());
        }

        public async Task<ServiceResultExt<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true)
        {
            var resultData = await _repo.GetAllAsync(@where, tracking);

            return new ServiceResultExt<IEnumerable<TEntity>>(resultObject: resultData, totalCount: resultData?.Count());
        }

        public async Task<ServiceResultExt<IEnumerable<TOut>>> GetAllAsync<TOut>(Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            var resultData = await _repo.GetAllAsync(@select, tracking);

            return new ServiceResultExt<IEnumerable<TOut>>(resultObject: resultData, totalCount: resultData?.Count());
        }

        public async Task<ServiceResultExt<IEnumerable<TEntity>>> GetAllAsync(bool tracking = true)
        {
            var resultData = await _repo.GetAllAsync(tracking);

            return new ServiceResultExt<IEnumerable<TEntity>>(resultObject: resultData, totalCount: resultData?.Count());
        }

        public async Task<ServiceResultExt<TEntity>> GetAsync(Expression<Func<TEntity, bool>> @where, bool tracking = true)
        {
            var resultData = await _repo.GetAsync(@where, tracking);
            var resultService = new ServiceResultExt<TEntity>(resultObject: resultData);

            if (resultData is null)
                resultService.Explanation = "The requested data was not found";

            return resultService;
        }

        public async Task<ServiceResultExt<TOut>> GetAsync<TOut>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            var resultData = await _repo.GetAsync(where, @select, tracking);
            var resultService = new ServiceResultExt<TOut>(resultObject: resultData);

            if (resultData is null)
                resultService.Explanation = "The requested data was not found";

            return resultService;
        }

        public async Task<ServiceResultExt<TEntity>> GetByIdAsync(object id, bool tracking = true)
        {
            var resultData = await _repo.GetByIdAsync(id, tracking);
            var resultService = new ServiceResultExt<TEntity>(resultObject: resultData);

            if (resultData is null)
                resultService.Explanation = "The requested data was not found";

            return resultService;
        }

        public async Task<ServiceResultExt<TOut>> GetByIdAsync<TOut>(object id, Expression<Func<TEntity, TOut>> @select, bool tracking = true)
        {
            var resultData = await _repo.GetByIdAsync(id, @select, tracking);
            var resultService = new ServiceResultExt<TOut>(resultObject: resultData);

            if (resultData is null)
                resultService.Explanation = "The requested data was not found";

            return resultService;
        }

        public async Task<ServiceResult> RemoveAsync(TEntity entity)
        {
            _repo.Remove(entity);
            var number = await _unitOfWork.SaveChangesAsync();

            if (number > 0)
                return new ServiceResult(explanation: "data is removed");

            return new ServiceResult(explanation: "no data was removed");
        }

        public async Task<ServiceResult> RemoveByIdAsync(object id)
        {
            await _repo.RemoveByIdAsync(id);
            var number = await _unitOfWork.SaveChangesAsync();

            if (number > 0)
                return new ServiceResult(explanation: "data is removed");

            return new ServiceResult(explanation: "no data was removed");
        }

        public async Task<ServiceResult> UpdateAsync(TEntity entity)
        {
            _repo.Update(entity);
            var number = await _unitOfWork.SaveChangesAsync();

            if (number > 0)
                return new ServiceResult(explanation: "data is updated");

            return new ServiceResult(explanation: "no data was updated");
        }

        public async Task<ServiceResult> UpdateRangeAsync(List<TEntity> entities)
        {
            _repo.UpdateRange(entities);
            var number = await _unitOfWork.SaveChangesAsync();

            if (number > 0)
                return new ServiceResult(explanation: "datas are updated");

            return new ServiceResult(explanation: "no data is updated");
        }
    }
}
