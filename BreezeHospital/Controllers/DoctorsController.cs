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
    public class DoctorsController : ControllerBase
    {
        public IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;

            
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _doctorService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _doctorService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{docId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetOperationsOfDoctor(int docId)
        {
            var data = await _doctorService.GetOperationsOfDoctorAsync(docId);
            return StatusCode(data.Status, data);
        }
        [HttpGet("{doccId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetPatientsOfDoctor(int doccId)
        {
            var data = await _doctorService.GetPatientsOfDoctorAsync(doccId);
            return StatusCode(data.Status, data);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetDoctorsBySpecialization(string spel)
        {
            var data = await _doctorService.GetBySpecialization(spel);
            return StatusCode(data.Status, data);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Add(DoctorsDto doctor)
        {
            var data = await _doctorService.AddAsync(doctor);
            return StatusCode(data.Status, data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int docId)
        {
            var data = await _doctorService.DeleteAsync(docId);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Update(DoctorsDto doctor, int id)
        {
            var data = await _doctorService.UpdateAsync(doctor, id);
            return StatusCode(data.Status, data);
        }
    }
}
