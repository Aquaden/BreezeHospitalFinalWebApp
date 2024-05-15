using Breeze.Application.DTOs.TokenDtos;
using Breeze.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IServices
{
    public interface ITokenHandler
    {
        Task<TokenDto> CreateAccessToken(AppUser user);
        string CresteRefreshToken();
    }
}
