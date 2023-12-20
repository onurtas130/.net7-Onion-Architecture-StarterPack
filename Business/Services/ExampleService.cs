using Application.Generics;
using Application.Services;
using Business.Generics;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ExampleService : GenericService<Example>, IExampleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExampleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
