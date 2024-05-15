using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Attributes;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezeHospitalWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorOperationController : ControllerBase
    {
        public IDoctorOperationService _doctorOperationService;

        public DoctorOperationController(IDoctorOperationService doctorOperationService)
        {
            _doctorOperationService = doctorOperationService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _doctorOperationService.GetAllAsync();
            return StatusCode(data.Status, data);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _doctorOperationService.GetByIdAsync(id);
            return StatusCode(data.Status, data);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        
        public async Task<IActionResult> Add(DoctorOperationAddDto docOpt)
        {
            var data = await _doctorOperationService.AddAsync(docOpt);
            return StatusCode(data.Status, data);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _doctorOperationService.DeleteAsync(Id);
            return StatusCode(data.Status, data);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [ValidateModel]
        public async Task<IActionResult> Update(DoctorOperationDto docOpt)
        {
            var data = await _doctorOperationService.UpdateAsync(docOpt);
            return StatusCode(data.Status, data);
        }
    }
}
