using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IDoctorOperationService
    {
        public Task<ResponseModel<List<DoctorOperationDto>>> GetAllAsync();
        public Task<ResponseModel<DoctorOperationDto>> GetByIdAsync(int id);
        
        public Task<ResponseModel<DoctorOperationAddDto>> AddAsync(DoctorOperationAddDto docOpt);
        public Task<ResponseModel<bool>> UpdateAsync(DoctorOperationDto doctOpt);
        public Task<ResponseModel<bool>> DeleteAsync(int docId);
        
    }
}
