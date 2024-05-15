using AutoMapper;
using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.DTOs.DoctorPatientDtos;
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
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<Doctors> _doctorRepository;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _doctorRepository = _unitOfWork.GetRepository<Doctors>();
        }

        public async Task<ResponseModel<DoctorsDto>> AddAsync(DoctorsDto doctor)
        {
            ResponseModel<DoctorsDto> responseModel = new ResponseModel<DoctorsDto>() { Data = null, Status = 400 };
            Doctors doct = _mapper.Map<Doctors>(doctor);
            if (doct != null)
            {
                await _doctorRepository.Add(doct);
                int rows = await _unitOfWork.SaveAsync();
                if (rows > 0)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doctor;
                }

            }
            return responseModel;

        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {

            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var analys = await _doctorRepository.DeleteById(id);
            var rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = true;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorsGetDto>>> GetAllAsync()
        {
            ResponseModel<List<DoctorsGetDto>> responseModel = new ResponseModel<List<DoctorsGetDto>>() { Data = null, Status = 400 };
            List<Doctors> doctors = await _doctorRepository.GetAll().ToListAsync();


            if (doctors != null && doctors.Count > 0)
            {
                var docts = _mapper.Map<List<DoctorsGetDto>>(doctors);
                if(docts != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = docts;

                }
                
            }
            return responseModel;

        }

        public async Task<ResponseModel<DoctorsGetDto>> GetByIdAsync(int id)
        {
            ResponseModel<DoctorsGetDto> responseModel = new ResponseModel<DoctorsGetDto>() { Data = null, Status = 400 };
            Doctors doctor = await _doctorRepository.GetByid(id); 


            if (doctor != null)
            {
                var doct = _mapper.Map<DoctorsGetDto>(doctor);
                if (doct != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doct;

                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorsGetDto>>> GetBySpecialization(string spname)
        {
            ResponseModel<List<DoctorsGetDto>> responseModel = new ResponseModel<List<DoctorsGetDto>>() { Data = null, Status = 400 };
            var spel = spname.ToUpper();
            List<Doctors> doctors =await _doctorRepository.GetAll().Where(x => x.Specialization.ToUpper() == spel).ToListAsync();


            if (doctors != null)
            {
                var doct = _mapper.Map<List<DoctorsGetDto>>(doctors);
                if (doct != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doct;

                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorOperationDto>>> GetOperationsOfDoctorAsync(int doctorId)
        {
            ResponseModel<List<DoctorOperationDto>> responseModel = new ResponseModel<List<DoctorOperationDto>>() { Data = null, Status = 400 };
            Doctors doctor = await _doctorRepository.GetByid(doctorId);


            if (doctor != null )
            {
                List<DoctorOperations> data =  doctor.DoctorsOperations.ToList();
                if (data.Count> 0)
                {
                    var data2 = _mapper.Map<List<DoctorOperationDto>>(data);
                    responseModel.Status = 200;
                    responseModel.Data = data2;

                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorPatientDto>>> GetPatientsOfDoctorAsync(int doctorId)
        {
            ResponseModel<List<DoctorPatientDto>> responseModel = new ResponseModel<List<DoctorPatientDto>>() { Data = null, Status = 400 };
            Doctors doctor = await _doctorRepository.GetByid(doctorId);


            if (doctor != null)
            {
                var data = doctor.DoctorsPatients.ToList();
                if (data.Count > 0)
                {
                    var data2 = _mapper.Map<List<DoctorPatientDto>>(data);
                    responseModel.Status = 200;
                    responseModel.Data = data2;

                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(DoctorsDto doctor, int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var olddoctor = await _doctorRepository.GetByid(id);
            if (olddoctor != null)
            {
                _mapper.Map<DoctorsDto, Doctors>(doctor, olddoctor);
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
