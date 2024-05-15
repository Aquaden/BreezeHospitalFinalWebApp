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
    public interface IOperationService
    {
        public Task<ResponseModel<List<OperationGetDto>>> GetAllAsync();
        public Task<ResponseModel<OperationGetDto>> GetByIdAsync(int optId);
        public Task<ResponseModel<List<OperationGetDto>>> GetByPatientIdAsync(int optId);
        public Task<ResponseModel<OperationDto>> AddAsync(OperationDto operation);
        public Task<ResponseModel<bool>> UpdateAsync(OperationDto operation, int id);
        public Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
