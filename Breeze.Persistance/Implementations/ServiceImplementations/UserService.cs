using AutoMapper;
using Breeze.Application.Abstractions.IServices;
using Breeze.Application.DTOs.IdentityDtos;
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
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {

            _userManager = userManager;
            _mapper = mapper;


        }


        public async Task<ResponseModel<CreateUserResponseDto>> AddAsync(CreateUserDto st)
        {
            ResponseModel<CreateUserResponseDto> responseModel = new ResponseModel<CreateUserResponseDto>() { Data = null, Status = 400 };
            var id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = id,
                LastName = st.LastName,
                FirstName = st.FirstName,
                Email = st.Email,
                UserName = st.UserName,

            }, st.Password);

            responseModel.Data = new CreateUserResponseDto { Succeed = result.Succeeded };
            responseModel.Status = result.Succeeded ? 200 : 400;
            if (!result.Succeeded)
            {
                responseModel.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
            }

            AppUser appUser = await _userManager.FindByEmailAsync(st.Email);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(st.UserName);
            }
            if (appUser == null)
            {
                appUser = await _userManager.FindByIdAsync(id);
            }
            if (appUser != null)
            {
                await _userManager.AddToRoleAsync(appUser, "User");
            }


            return responseModel;
        }

        public async Task<ResponseModel<bool>> AssignRolesToUserAsync(string userid, string[] roles)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return responseModel;
            }

            var getroles = await _userManager.GetRolesAsync(user);
            var roless = await _userManager.RemoveFromRolesAsync(user, getroles);
            if (!getroles.ToList().Contains("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");

            }
            await _userManager.AddToRolesAsync(user, roles);

            responseModel.Status = 200;
            responseModel.Data = true;

            return responseModel;

        }

        public async Task<ResponseModel<bool>> DeleteAsync(string idOrName)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var user = await _userManager.FindByIdAsync(idOrName);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(idOrName);
            }
            if (user == null)
            {
                return responseModel;
            }
            IdentityResult res = await _userManager.DeleteAsync(user);

            if (res == IdentityResult.Success)
            {
                responseModel.Data = true;
                responseModel.Status = 200;

            }



            return responseModel;
        }

        public async Task<ResponseModel<List<GetUserDto>>> GetAllUserAsync()
        {
            ResponseModel<List<GetUserDto>> responseModel = new ResponseModel<List<GetUserDto>>();
            var data = await _userManager.Users.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var users = _mapper.Map<List<GetUserDto>>(data);
                responseModel.Data = users;
                responseModel.Status = 200;
            }
            else
            {
                responseModel.Status = 400;
                responseModel.Data = null;
            }




            return responseModel;
        }

        public async Task<ResponseModel<GetUserDto>> GetById(string id)
        {
            ResponseModel<GetUserDto> responseModel = new ResponseModel<GetUserDto>();
            var data = await _userManager.FindByIdAsync(id);

            if (data != null)
            {
                var user = _mapper.Map<GetUserDto>(data);
                responseModel.Data = user;
                responseModel.Status = 200;
            }
            else
            {
                responseModel.Status = 400;
                responseModel.Data = null;
            }
            return responseModel;


        }

        public async Task<ResponseModel<string[]>> GetRolesToUserAsync(string usernameorid)
        {
            ResponseModel<string[]> response = new ResponseModel<string[]>() { Data = null, Status = 400 };
            var user = await _userManager.FindByNameAsync(usernameorid);
            if (user == null)
            {
                user = await _userManager.FindByIdAsync(usernameorid);

            }
            if (user == null)
            {
                return response;
            }
            var data = await _userManager.GetRolesAsync(user);
            if (data != null && data.Count > 0)
            {
                response.Status = 200;
                response.Data = data.ToArray();
            }

            return response;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(UpdateUserDto st)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var user = await _userManager.FindByIdAsync(st.Id);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(st.UserName);
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Id = st.Id;
            user.FirstName = st.FirstName;
            user.LastName = st.LastName;
            user.UserName = st.UserName;
            user.BirthDay = st.BirthDay;
            user.Email = st.Email;
            IdentityResult res = await _userManager.UpdateAsync(user);

            if (res == IdentityResult.Success)
            {
                responseModel.Data = true;
                responseModel.Status = 200;
            }

            return responseModel;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime dateTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = dateTime.AddMinutes(10);
                await _userManager.UpdateAsync(user);
            }

        }
    }
}
