using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IDoctorPatientService
    {
        public Task<ResponseModel<List<DoctorPatientDto>>> GetAllAsync();
        public Task<ResponseModel<DoctorPatientDto>> GetByIdAsync(int id);
        public Task<ResponseModel<DoctorPatientAddDto>> AddAsync(DoctorPatientAddDto doctorPatient);
        
        public Task<ResponseModel<bool>> UpdateAsync(DoctorPatientAddDto doctorPatient,int id);
        public Task<ResponseModel<bool>> DeleteAsync(int id);

    }
}
