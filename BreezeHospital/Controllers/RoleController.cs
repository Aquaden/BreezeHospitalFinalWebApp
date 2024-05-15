using Breeze.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }
        [HttpPost]//url kicikle yazilir
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddAsync(string name)
        {
            var data = await _roleService.AddRole(name);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(string id, string name)
        {
            var data = await _roleService.UpdateAsync(id, name);
            return StatusCode(data.Status, data);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var data = await _roleService.DeleteRole(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _roleService.GetRolesById(id);
            return StatusCode(data.Status, data);

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _roleService.GetAllRoles();
            return StatusCode(data.Status, data);
        }

    }
}
