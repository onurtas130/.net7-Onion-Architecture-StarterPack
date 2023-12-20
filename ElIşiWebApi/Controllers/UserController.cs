using Application.Services;
using Domain.Entities.Identity;
using ElIşiWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElIşiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserVM createUserVM)
        {
            var user = new AppUser()
            {
                UserName = createUserVM.UserName,
                Email = createUserVM.Email,
            };

            var serviceResult = await _appUserService.RegisterAsync(user, createUserVM.password);

            if (serviceResult.Status)
                return Ok(serviceResult);

            return BadRequest(serviceResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var serviceResult = await _appUserService.LoginAsync(loginVM.Email, loginVM.Password);

            if (serviceResult.Status)
                return Ok(serviceResult);

            return BadRequest(serviceResult);
        }
    }
}
