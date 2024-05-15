using Breeze.Application.DTOs;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IPatientService
    {
        public Task<ResponseModel<List<PatientsGetDto>>> GetAllAsync();
        public Task<ResponseModel<PatientsGetDto>> GetByIdAsync(int id);
        //public Task<ResponseModel<PatientsDto>> GetByName(string name);
        public Task<ResponseModel<List<DoctorsGetDto>>> GetDoctorsOfPatientAsync(int patientId);
        public Task<ResponseModel<List<OperationGetDto>>> GetOperationsOfPatientAsync(int patientId);
        public Task<ResponseModel<PatientsDto>> AddAsync(PatientsDto patient);
        public Task<ResponseModel<bool>> UpdateAsync(PatientsDto patient, int id);
        public Task<ResponseModel<bool>> DeleteAsync(int id);
        //getanalyses yoxdu cunki analysservice de hemin metod var!
    }
}
