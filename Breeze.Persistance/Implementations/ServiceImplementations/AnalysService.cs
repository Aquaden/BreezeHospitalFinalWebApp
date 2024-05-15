using AutoMapper;
using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IServices;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Application.DTOs;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class AnalysService : IAnalysService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<Analyses> _analysRepository;

        public AnalysService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _analysRepository = _unitOfWork.GetRepository<Analyses>();
                
        }
        public async Task<ResponseModel<AnalysDto>> AddAsync(AnalysDto analys)
        {
            ResponseModel<AnalysDto> responseModel = new ResponseModel<AnalysDto>() { Data = null,Status =404 };
            Analyses analyss = _mapper.Map<Analyses>(analys);
            await _analysRepository.Add(analyss);
            var rows = await _unitOfWork.SaveAsync();
            if(rows > 0) 
            {
                responseModel.Status = 200;
                responseModel.Data = analys;
            }

            return responseModel;

        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var analys = await _analysRepository.DeleteById(id);
            var rows = await _unitOfWork.SaveAsync();
            if(rows > 0)
            {
                responseModel.Status = 200;
                responseModel.Data = true;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<AnalysGetDto>>> GetAllAsync()
        {
            ResponseModel<List<AnalysGetDto>> responseModel = new ResponseModel<List<AnalysGetDto>>() { Data = null, Status = 404 };
            List<Analyses> analyses = await _analysRepository.GetAll().Include(a=> a.Patient).ToListAsync();
            if(analyses != null && analyses.Count > 0)
            {
                var data = _mapper.Map<List<AnalysGetDto>>(analyses);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;

        }

        public async Task<ResponseModel<List<AnalysGetDto>>> GetAllAnalysByPatientIdAsync(int patientId)
        {
            ResponseModel<List<AnalysGetDto>> responseModel = new ResponseModel<List<AnalysGetDto>>() { Data = null, Status = 404 };
            List<Analyses> analyses = await _analysRepository.GetAll().Where(x => x.PatientId == patientId).ToListAsync();
            if(analyses != null && analyses.Count>0)
            {
                var data = _mapper.Map<List<AnalysGetDto>>(analyses);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<AnalysGetDto>> GetByIdAsync(int id)
        {
            ResponseModel<AnalysGetDto> responseModel = new ResponseModel<AnalysGetDto>() { Data = null, Status = 404 };
            Analyses analys = await _analysRepository.GetByid(id);
            if(analys != null)
            {
                var data = _mapper.Map<AnalysGetDto>(analys);
                responseModel.Status = 200;
                responseModel.Data = data;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(AnalysDto analys,int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var analyss = await _analysRepository.GetByid(id);
            if(analyss != null)
            {
                _mapper.Map<AnalysDto, Analyses>(analys, analyss);
                var rows = await _unitOfWork.SaveAsync();
                if(rows>0)
                {
                    responseModel.Status = 200;
                    responseModel.Data = true;
                }
            }
            return responseModel;
        }
    }
}
