using Breeze.Application.DTOs.IdentityDtos;
using Breeze.Application.MyModels;
using Breeze.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IUserService
    {
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime dateTime);
        public Task<ResponseModel<bool>> AssignRolesToUserAsync(string userid, string[] roles);
        public Task<ResponseModel<List<GetUserDto>>> GetAllUserAsync();
        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string usernameorid);
        public Task<ResponseModel<CreateUserResponseDto>> AddAsync(CreateUserDto st);
        public Task<ResponseModel<bool>> UpdateAsync(UpdateUserDto st);
        public Task<ResponseModel<bool>> DeleteAsync(string idOrName);
        public Task<ResponseModel<GetUserDto>> GetById(string id);
    }
}
