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
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class OperationService : IOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<Operations> _operationRepository;

        public OperationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _operationRepository = _unitOfWork.GetRepository<Operations>();
        }

        public async Task<ResponseModel<OperationDto>> AddAsync(OperationDto operation)
        {
            ResponseModel<OperationDto> responseModel = new ResponseModel<OperationDto>() { Data = null, Status = 404 };
            Operations newOperation = _mapper.Map<Operations>(operation);
            await _operationRepository.Add(newOperation);
            var rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = operation;
            }

            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var operation = await _operationRepository.DeleteById(id);
            var rows = await _unitOfWork.SaveAsync();
            if (rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = true;
            }
            return responseModel;
        }


        public async Task<ResponseModel<List<OperationGetDto>>> GetAllAsync()
        {
            ResponseModel<List<OperationGetDto>> responseModel = new ResponseModel<List<OperationGetDto>>() { Data = null, Status = 404 };
            List<Operations> operations = await _operationRepository.GetAll().ToListAsync();
            if (operations != null && operations.Count > 0)
            {
                var data = _mapper.Map<List<OperationGetDto>>(operations);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<OperationGetDto>> GetByIdAsync(int operationId)
        {
            ResponseModel<OperationGetDto> responseModel = new ResponseModel<OperationGetDto>() { Data = null, Status = 404 };
            Operations opt = await _operationRepository.GetByid(operationId);
            if (opt != null)
            {
                var data = _mapper.Map<OperationGetDto>(opt);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<OperationGetDto>>> GetByPatientIdAsync(int operationId)
        {
            ResponseModel<List<OperationGetDto>> responseModel = new ResponseModel<List<OperationGetDto>>() { Data = null, Status = 404 };
            List<Operations> operations = await _operationRepository.GetAll().Where(x => x.PatientId == operationId).ToListAsync();
            if (operations != null && operations.Count > 0)
            {
                var data = _mapper.Map<List<OperationGetDto>>(operations);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(OperationDto operation, int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var opt = await _operationRepository.GetByid(id);
            if (opt != null)
            {
                _mapper.Map<OperationDto, Operations>(operation, opt);
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
