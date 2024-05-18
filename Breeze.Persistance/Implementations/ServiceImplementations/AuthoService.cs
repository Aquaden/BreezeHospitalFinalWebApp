using Breeze.Application.Abstractions.IServices;
using Breeze.Application.DTOs.TokenDtos;
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
    public class AuthoService : IAuthoService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;

        public AuthoService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<ResponseModel<TokenDto>> LoginAsync(string usernameOrEmail, string password)
        {
            ResponseModel<TokenDto> responseModel = new ResponseModel<TokenDto>() { Data = null, Status = 404 };
            var user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(usernameOrEmail);
            }
            var signinres = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (signinres.Succeeded)
            {
                if (user != null)
                {
                    var token = await _tokenHandler.CreateAccessToken(user);
                    await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expired);

                    responseModel.Status = 200;
                    responseModel.Data = token;




                }

            }

            return responseModel;
        }

        public async Task<ResponseModel<TokenDto>> CreateNewTokenAsync(string refreshToken)
        {
            ResponseModel<TokenDto> responseModel = new ResponseModel<TokenDto>() { Data = null, Status = 404 };
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
            {
                var token = await _tokenHandler.CreateAccessToken(user);

                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expired);

                responseModel.Status = 200;
                responseModel.Data = token;


            }
            else
            {
                responseModel.Data = null;
                responseModel.Status = 401;
            }
            return responseModel;

        }

        public async Task<ResponseModel<bool>> LogOutAsync(string userNameOrEmail)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 404 };
            var user = await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userNameOrEmail);

            }
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenEndTime = null;
                var res = await _userManager.UpdateAsync(user);
                await _signInManager.SignOutAsync();
                if (res.Succeeded)
                {
                    responseModel.Data = true;
                    responseModel.Status = 200;
                }

            }
            else
            {
                responseModel.Data = false;
                responseModel.Status = 401;
            }

            return responseModel;
        }

        public async Task<ResponseModel<bool>> PasswordResetAsync(string email, string currentPassword, string newPassword)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var changePassword = _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (changePassword.IsCompletedSuccessfully)
                {
                    responseModel.Data = true;
                    responseModel.Status = 200;
                }

            }
            return responseModel;
        }
    }
}
