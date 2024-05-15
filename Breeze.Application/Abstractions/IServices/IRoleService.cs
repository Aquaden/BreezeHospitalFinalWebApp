using Breeze.Application.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IRoleService
    {

        public Task<ResponseModel<object>> GetAllRoles();
        public Task<ResponseModel<bool>> AddRole(string name);
        public Task<ResponseModel<object>> GetRolesById(string id);
        public Task<ResponseModel<bool>> DeleteRole(string st);
        public Task<ResponseModel<bool>> UpdateAsync(string id, string name);
    }
}
