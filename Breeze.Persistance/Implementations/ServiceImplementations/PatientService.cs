using AutoMapper;
using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Application.DTOs;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<Patients> _patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _patientRepository = _unitOfWork.GetRepository<Patients>();
        }

        public async Task<ResponseModel<PatientsDto>> AddAsync(PatientsDto patient)
        {
            ResponseModel<PatientsDto> responseModel = new ResponseModel<PatientsDto>() { Data = null, Status = 404 };
            Patients newPatient = _mapper.Map<Patients>(patient);
            await _patientRepository.Add(newPatient);
            var rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = patient;
            }

            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var analys = await _patientRepository.DeleteById(id);
            var rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = true;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<PatientsGetDto>>> GetAllAsync()
        {
            ResponseModel<List<PatientsGetDto>> responseModel = new ResponseModel<List<PatientsGetDto>>() { Data = null, Status = 404 };
            List<Patients> patients = await _patientRepository.GetAll().ToListAsync();
            if (patients != null && patients.Count > 0)
            {
                var data = _mapper.Map<List<PatientsGetDto>>(patients);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<PatientsGetDto>> GetByIdAsync(int id)
        {
            ResponseModel<PatientsGetDto> responseModel = new ResponseModel<PatientsGetDto>() { Data = null, Status = 404 };
            Patients patient = await _patientRepository.GetByid(id);
            if (patient != null)
            {
                var data = _mapper.Map<PatientsGetDto>(patient);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorsGetDto>>> GetDoctorsOfPatientAsync(int pId)
        {
            ResponseModel<List<DoctorsGetDto>> responseModel = new ResponseModel<List<DoctorsGetDto>>() { Data = null, Status = 404 };
            Patients patient = await _patientRepository.GetByid(pId);

            if (patient != null)
            {
                // Retrieve the doctors associated with the patient
                List<Doctors> doctors = patient.DoctorsPatients.Select(dp => dp.Doctor).ToList();
                if(doctors != null)
                {
                    var data = _mapper.Map<List<DoctorsGetDto>>(doctors);
                    responseModel.Status = 200;
                    responseModel.Data = data;

                }
                

                
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<OperationGetDto>>> GetOperationsOfPatientAsync(int pId)
        {
            ResponseModel<List<OperationGetDto>> responseModel = new ResponseModel<List<OperationGetDto>>() { Data = null, Status = 404 };
            Patients patient = await _patientRepository.GetByid(pId);

            if (patient != null)
            {

                List<Operations> operations =  patient.Operation.Where(x => x.PatientId == pId).ToList();
                if(operations.Count > 0 && operations != null)
                {
                    var data = _mapper.Map<List<OperationGetDto>>(operations);
                    responseModel.Status = 200;
                    responseModel.Data = data;

                }

                
                
            }
            return responseModel;
            
        }

        public async Task<ResponseModel<bool>> UpdateAsync(PatientsDto patient, int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var oldpatient = await _patientRepository.GetByid(id);
            if (oldpatient != null)
            {
                _mapper.Map<PatientsDto, Patients>(patient, oldpatient);
                var rows = await _unitOfWork.SaveAsync();
                if (rows > 0)
                {
                    responseModel.Status = 200;
                    responseModel.Data = true;
                }
            }
            return responseModel;
        }
    }
}
