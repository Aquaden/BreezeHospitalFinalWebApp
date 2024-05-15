using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        public IOperationService _operationservice;

        public OperationsController(IOperationService operationservice)
        {
            _operationservice = operationservice;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _operationservice.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _operationservice.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{pId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetOperationsByPatientId(int pId)
        {
            var data = await _operationservice.GetByPatientIdAsync(pId);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        
        public async Task<IActionResult> Add(OperationDto opt)
        {
            var data = await _operationservice.AddAsync(opt);
            return StatusCode(data.Status, data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int optId)
        {
            var data = await _operationservice.DeleteAsync(optId);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        
        public async Task<IActionResult> Update(OperationDto opt, int id)
        {
            var data = await _operationservice.UpdateAsync(opt, id);
            return StatusCode(data.Status, data);
        }
    }
}
