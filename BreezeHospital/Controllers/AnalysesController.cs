using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysesController : ControllerBase
    {
        public IAnalysService _analysService;
        public AnalysesController(IAnalysService analysService)
        {
                
            _analysService = analysService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _analysService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _analysService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{pId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAnalysesByPatiensId(int pId)
        {
            var data = await _analysService.GetAllAnalysByPatientIdAsync(pId);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Add(AnalysDto analys)
        {
            var data = await _analysService.AddAsync(analys);
            return Ok(data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int analysId)
        {
            var data = await _analysService.DeleteAsync(analysId);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Update(AnalysDto analys,int id)
        {
            var data = await _analysService.UpdateAsync(analys,id);
            return StatusCode(data.Status, data);
        }
    }
}
