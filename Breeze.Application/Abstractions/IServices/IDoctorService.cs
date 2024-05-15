using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IDoctorService
    {
        public Task<ResponseModel<List<DoctorsGetDto>>> GetAllAsync();
        public Task<ResponseModel<DoctorsGetDto>> GetByIdAsync(int id);
        public Task<ResponseModel<List<DoctorsGetDto>>> GetBySpecialization(string specialization);
        public Task<ResponseModel<List<DoctorPatientDto>>> GetPatientsOfDoctorAsync(int id);
        public Task<ResponseModel<List<DoctorOperationDto>>> GetOperationsOfDoctorAsync(int id);
        public Task<ResponseModel<DoctorsDto>> AddAsync(DoctorsDto doctor);
        public Task<ResponseModel<bool>> UpdateAsync(DoctorsDto doctor, int id);
        public Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
