using Application.Generics;
using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Generics
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity => new GenericRepository<TEntity>(_dbContext);

        public IExampleRepository ExampleRepository => new ExampleRepository(_dbContext);

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
