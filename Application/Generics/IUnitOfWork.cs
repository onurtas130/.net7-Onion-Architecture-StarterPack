using Application.Repositories;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Generics
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        IExampleRepository ExampleRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
