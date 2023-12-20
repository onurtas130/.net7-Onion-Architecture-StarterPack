using Application.Consts;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElIşiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _exampleService.GetAllAsync());
        }

        [Authorize(Roles = UserRoles.Member + "," + UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post(Example example)
        {
            var serviceResult = await _exampleService.AddAsync(example);

            if (serviceResult.Status)
                return Ok(serviceResult);

            return BadRequest(serviceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _exampleService.RemoveByIdAsync(id);

            if(serviceResult.Status)
                return Ok(serviceResult);

            return BadRequest(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Example example)
        {
            var serviceResult = await _exampleService.UpdateAsync(example);

            if(serviceResult.Status)
                return Ok(serviceResult);

            return BadRequest(serviceResult);   
        }

    }
}
