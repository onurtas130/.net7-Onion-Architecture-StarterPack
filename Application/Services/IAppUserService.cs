using Application.DTOs;
using Domain.Entities.Identity;
using Domain.GeneralModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAppUserService
    {
        public Task<ServiceResult> RegisterAsync(AppUser appUser, string password);

        public Task<ServiceResultExt<JWTResultDTO>> LoginAsync(string email, string password);
    }
}
