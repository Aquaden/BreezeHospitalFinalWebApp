using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs;
using Breeze.Persistance.Implementations.ServiceImplementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _patientService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _patientService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{patientId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetDoctorsOfPatient(int patientId)
        {
            var data = await _patientService.GetDoctorsOfPatientAsync(patientId);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{patientId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOperationsOfPatient(int patientId)
        {
            var data = await _patientService.GetOperationsOfPatientAsync(patientId);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ValidateModel]
        public async Task<IActionResult> Add(PatientsDto patient)
        {
            var data = await _patientService.AddAsync(patient);
            return StatusCode(data.Status, data);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _patientService.DeleteAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ValidateModel]
        public async Task<IActionResult> Update(PatientsDto patient, int id)
        {
            var data = await _patientService.UpdateAsync(patient, id);
            return StatusCode(data.Status, data);
        }
    }
}
