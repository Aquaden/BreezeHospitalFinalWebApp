using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs.IdentityDtos;
using Breeze.Application.MyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost]//url kicikle yazilir
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> AddAsync(CreateUserDto student)
        {
            var data = await _userService.AddAsync(student);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync(UpdateUserDto student)
        {
            var data = await _userService.UpdateAsync(student);
            return StatusCode(data.Status, data);
        }
        [HttpDelete("{idOrName}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> DeleteAsync(string idOrName)
        {
            var data = await _userService.DeleteAsync(idOrName);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{usernameorid}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetRolesToUserAsync(string usernameorid)
        {
            var data = await _userService.GetRolesToUserAsync(usernameorid);
            return StatusCode(data.Status, data);

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _userService.GetAllUserAsync();
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> AssignRolesToUserAsync(string userid, string[] roles) => await _userService.AssignRolesToUserAsync(userid, roles) is ResponseModel<bool> data ? StatusCode(data.Status, data) : NotFound();


    }
}
