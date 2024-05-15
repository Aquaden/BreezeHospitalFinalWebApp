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
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class DoctorOperationService :IDoctorOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<DoctorOperations> _docOprService;
        private IGenericRepository<Doctors> _docService;

        public DoctorOperationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _docOprService = _unitOfWork.GetRepository<DoctorOperations>();
            _docService = _unitOfWork.GetRepository<Doctors>();
        }

        public async Task<ResponseModel<DoctorOperationAddDto>> AddAsync(DoctorOperationAddDto doctorOperation)
        {
            ResponseModel<DoctorOperationAddDto> responseModel = new ResponseModel<DoctorOperationAddDto>() { Data = null, Status = 400 };
            DoctorOperations doctopt = _mapper.Map<DoctorOperations>(doctorOperation);
            if (doctopt != null)
            {
                await _docOprService.Add(doctopt);
                int rows = await _unitOfWork.SaveAsync();
                if (rows > 0)
                {
                    responseModel.Status = 200;
                    responseModel.Data = doctorOperation;
                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int doctorId)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            await _docOprService.DeleteById(doctorId);
            int rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {

                responseModel.Status = 200;
                responseModel.Data = true;


            }
            return responseModel;
        }

        public async Task<ResponseModel<List<DoctorOperationDto>>> GetAllAsync()
        {
            ResponseModel<List<DoctorOperationDto>> responseModel = new ResponseModel<List<DoctorOperationDto>>() { Data = null, Status = 400 };
            List<DoctorOperations> docOpr = await _docOprService.GetAll().ToListAsync();

            if (docOpr != null && docOpr.Count > 0)
            {
                List<DoctorOperationDto> docOp = _mapper.Map<List<DoctorOperationDto>>(docOpr);

                if (docOp != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = docOp;
                }

            }
            return responseModel;
        }

        public async Task<ResponseModel<DoctorOperationDto>> GetByIdAsync(int id)
        {
            ResponseModel<DoctorOperationDto> responseModel = new ResponseModel<DoctorOperationDto>() { Data = null, Status = 400 };
            DoctorOperations docOpr = await _docOprService.GetByid(id);

            if (docOpr != null )
            {
                DoctorOperationDto docOp = _mapper.Map<DoctorOperationDto>(docOpr);

                if (docOp != null)
                {
                    responseModel.Status = 200;
                    responseModel.Data = docOp;
                }

            }
            return responseModel;
        }

       

        public async Task<ResponseModel<bool>> UpdateAsync(DoctorOperationAddDto doctorOperation,int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var olddata = await _docOprService.GetByid(id);
            if (olddata != null)
            {
                _mapper.Map<DoctorOperationAddDto, DoctorOperations>(doctorOperation, olddata);
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
