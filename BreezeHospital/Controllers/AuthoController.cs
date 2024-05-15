using Breeze.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthoController : ControllerBase
    {
        private readonly IAuthoService _authoService;

        public AuthoController(IAuthoService authoService)
        {
            _authoService = authoService;
        }
        [HttpPost]//url kicikle yazilir
        public async Task<IActionResult> Login(string usernameOrEmail, string password)
        {
            var data = await _authoService.LoginAsync(usernameOrEmail, password);
            return StatusCode(data.Status, data);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public async Task<IActionResult> LogOut(string usernameOremail)
        {
            var data = await _authoService.LogOutAsync(usernameOremail);
            return StatusCode(data.Status, data);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string email, string curPas, string newPas)
        {
            var data = await _authoService.PasswordResetAsync(email, curPas, newPas);
            return StatusCode(data.Status, data);
        }
        [HttpGet]
        public async Task<IActionResult> CreateNewRefreshToken(string refreshtoken)//deyish
        {
            var data = await _authoService.CreateNewResreshTokenAsync(refreshtoken);
            return StatusCode(data.Status, data);

        }
    }
}
