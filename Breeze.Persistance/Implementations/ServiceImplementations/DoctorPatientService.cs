using AutoMapper;
using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class DoctorPatientService : IDoctorPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<DoctorPatients> _docPatRepository;

        public DoctorPatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _docPatRepository = _unitOfWork.GetRepository<DoctorPatients>();
        }

        public async Task<ResponseModel<DoctorPatientAddDto>> AddAsync(DoctorPatientAddDto docPat)
        {
            ResponseModel<DoctorPatientAddDto> responseModel = new ResponseModel<DoctorPatientAddDto>() { Data = null,Status = 400};
            DoctorPatients doctPat = _mapper.Map<DoctorPatients>(docPat);
            if (doctPat != null)
            {
                await _docPatRepository.Add(doctPat);
                int rows = await _unitOfWork.SaveAsync();
                if (rows > 0)
                {
                    responseModel.Status = 200;
                    responseModel.Data = docPat;
                }

            }
            return responseModel;
        }

        

        public async Task<ResponseModel<bool>> DeleteAsync(int Id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            await _docPatRepository.DeleteById(Id);
            int rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                
                    responseModel.Status = 200;
                    responseModel.Data = true;
                

            }
            return responseModel;
        }

        

        public async Task<ResponseModel<List<DoctorPatientDto>>> GetAllAsync()
        {
            ResponseModel<List<DoctorPatientDto>> responseModel = new ResponseModel<List<DoctorPatientDto>>() { Data = null, Status = 400 };
            List<DoctorPatients> docPats = await _docPatRepository.GetAll().ToListAsync();
            
            if (docPats != null && docPats.Count > 0)
            {
                List<DoctorPatientDto> doctPat = _mapper.Map<List<DoctorPatientDto>>(docPats);
                
                if (doctPat != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doctPat;
                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<DoctorPatientDto>> GetByIdAsync(int id)
        {
            ResponseModel<DoctorPatientDto> responseModel = new ResponseModel<DoctorPatientDto>() { Data = null, Status = 400 };
            DoctorPatients docPat = await _docPatRepository.GetByid(id);

            if (docPat != null )
            {
                DoctorPatientDto doctPat = _mapper.Map<DoctorPatientDto>(docPat);

                if (doctPat != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doctPat;
                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(DoctorPatientDto docpat)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var olddata = await _docPatRepository.GetByid(docpat.Id);
            if (olddata != null)
            {
                _mapper.Map<DoctorPatientDto, DoctorPatients>(docpat, olddata);
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
