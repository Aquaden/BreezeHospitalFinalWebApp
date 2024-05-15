using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs;
using Breeze.Persistance.Implementations.ServiceImplementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _patientService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _patientService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{pId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetDoctorsOfPatient(int pId)
        {
            var data = await _patientService.GetDoctorsOfPatientAsync(pId);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{patientId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetOperationsOfPatient(int patientId)
        {
            var data = await _patientService.GetOperationsOfPatientAsync(patientId);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Add(PatientsDto patient)
        {
            var data = await _patientService.AddAsync(patient);
            return StatusCode(data.Status, data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int patId)
        {
            var data = await _patientService.DeleteAsync(patId);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Update(PatientsDto patient, int id)
        {
            var data = await _patientService.UpdateAsync(patient, id);
            return StatusCode(data.Status, data);
        }
    }
}
