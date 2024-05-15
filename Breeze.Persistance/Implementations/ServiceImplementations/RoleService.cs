using Breeze.Application.Abstractions.IServices;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleService;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleService = roleManager;

        }

        public async Task<ResponseModel<bool>> AddRole(string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>()
            {
                Data = true,
                Status = 400
            };
            string id = Guid.NewGuid().ToString();
            IdentityResult result = await _roleService.CreateAsync(new() { Id = id, Name = name });
            if (result.Succeeded)
            {
                responseModel.Data = true;
                responseModel.Status = 200;
            }
            return responseModel;



        }

        public async Task<ResponseModel<bool>> DeleteRole(string id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var data = await _roleService.FindByIdAsync(id);
            if (data != null)
            {
                IdentityResult res = await _roleService.DeleteAsync(data);
                if (res.Succeeded)
                {
                    responseModel.Status = 200;
                    responseModel.Data = true;
                }

            }
            return responseModel;

        }

        public async Task<ResponseModel<object>> GetAllRoles()
        {
            ResponseModel<object> responseModel = new ResponseModel<object>() { Data = null, Status = 400 };
            var data = await _roleService.Roles.ToListAsync();
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.Status = 200;
            }
            return responseModel;
        }

        public async Task<ResponseModel<object>> GetRolesById(string id)
        {
            ResponseModel<object> responseModel = new ResponseModel<object>() { Data = null, Status = 400 };
            var data = await _roleService.FindByIdAsync(id);
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.Status = 200;
            }
            return responseModel;

        }

        public async Task<ResponseModel<bool>> UpdateAsync(string id, string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var data = await _roleService.FindByIdAsync(id);
            if (data != null)
            {
                data.Name = name;
                IdentityResult res = await _roleService.UpdateAsync(data);
                if (res.Succeeded)
                {
                    responseModel.Status = 200;
                    responseModel.Data = true;
                }

            }
            return responseModel;
        }
    }
}
