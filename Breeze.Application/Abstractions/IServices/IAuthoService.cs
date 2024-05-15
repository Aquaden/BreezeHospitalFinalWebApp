using Breeze.Application.DTOs.TokenDtos;
using Breeze.Application.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface IAuthoService
    {
        public Task<ResponseModel<TokenDto>> LoginAsync(string usernameOrEmail, string password);
        public Task<ResponseModel<TokenDto>> LoginWithResreshTokenAsync(string refreshToken);
        public Task<ResponseModel<bool>> LogOutAsync(string userNameOrEmail);
        public Task<ResponseModel<bool>> PasswordResetAsync(string email, string curPas, string newPas);

    }
}
