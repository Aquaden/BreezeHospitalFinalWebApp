using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorPatientController : ControllerBase
    {
        public IDoctorPatientService _doctPatService;

        public DoctorPatientController(IDoctorPatientService doctPatService)
        {
            _doctPatService = doctPatService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _doctPatService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _doctPatService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Add(DoctorPatientAddDto docPat)
        {
            var data = await _doctPatService.AddAsync(docPat);
            return StatusCode(data.Status, data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _doctPatService.DeleteAsync(Id);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Update(DoctorPatientDto docpat)
        {
            var data = await _doctPatService.UpdateAsync(docpat);
            return StatusCode(data.Status, data);
        }
    }
}
