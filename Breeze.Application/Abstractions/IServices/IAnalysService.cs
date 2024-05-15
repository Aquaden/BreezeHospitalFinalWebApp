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
    public interface IAnalysService
    {
        public Task<ResponseModel<List<AnalysGetDto>>> GetAllAsync();
        public Task<ResponseModel<AnalysGetDto>> GetByIdAsync(int id);
        public Task<ResponseModel<List<AnalysGetDto>>> GetAllAnalysByPatientIdAsync(int pId);
        public Task<ResponseModel<AnalysDto>> AddAsync(AnalysDto analys);
        public Task<ResponseModel<bool>> UpdateAsync(AnalysDto analys,int id);
        public Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
