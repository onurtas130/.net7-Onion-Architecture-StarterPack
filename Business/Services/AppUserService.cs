using Application.Consts;
using Application.DTOs;
using Application.Services;
using Business.Helpers;
using Domain.Entities.Identity;
using Domain.GeneralModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        public AppUserService(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ServiceResultExt<JWTResultDTO>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var checkResult = await _userManager.CheckPasswordAsync(user, password);

                if (checkResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtResult = JWTHelper.GenerateJwtToken(user, _configuration["JWTToken:SecretKey"], roles);

                    return new ServiceResultExt<JWTResultDTO>(explanation: "Login is succes", resultObject: jwtResult);
                }

                return new ServiceResultExt<JWTResultDTO>(explanation: "email or password is wrong", resultObject: null, status: false);
            }

            return new ServiceResultExt<JWTResultDTO>(explanation: "email or password is wrong", resultObject: null, status: false);
        }

        public async Task<ServiceResult> RegisterAsync(AppUser appUser, string password)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);

            if (user == null)
            {
                var createResult = await _userManager.CreateAsync(appUser, password);

                if (createResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, UserRoles.Member);

                    if (roleResult.Succeeded)
                    {
                        return new ServiceResult(explanation: "user is registered");
                    }

                    return new ServiceResult(explanation: "some error occured on registering authorize");
                }

                return new ServiceResult(explanation: "user wasn't registered");
            }

            return new ServiceResult(explanation: "email is already taken by someone, try new one");
        }
    }
}
