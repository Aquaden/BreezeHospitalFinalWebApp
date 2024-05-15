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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _operationservice.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _operationservice.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{patientId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOperationsByPatientId(int patientId)
        {
            var data = await _operationservice.GetByPatientIdAsync(patientId);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ValidateModel]

        public async Task<IActionResult> Add(OperationDto operation)
        {
            var data = await _operationservice.AddAsync(operation);
            return StatusCode(data.Status, data);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _operationservice.DeleteAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ValidateModel]
        
        public async Task<IActionResult> Update(OperationDto operation, int id)
        {
            var data = await _operationservice.UpdateAsync(operation, id);
            return StatusCode(data.Status, data);
        }
    }
}
