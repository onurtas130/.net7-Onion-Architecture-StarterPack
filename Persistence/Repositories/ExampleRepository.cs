using Application.Repositories;
using Domain.Entities;
using Persistence.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ExampleRepository : GenericRepository<Example>, IExampleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExampleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
    }
}
