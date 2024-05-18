using Breeze.Application.Abstractions.IServices;
using Breeze.Application.DTOs.TokenDtos;
using Breeze.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.ServiceImplementations
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;

        }
        public async Task<TokenDto> CreateAccessToken(AppUser user)
        {
            TokenDto tokenDto = new TokenDto();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {

                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Id)


            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            tokenDto.Expired = DateTime.UtcNow.AddMinutes(15);
            JwtSecurityToken securityToken = new(
                audience: _configuration["JWT:Audience"],
                issuer: _configuration["JWT:Issuer"],
                expires: tokenDto.Expired,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );
            //token yaradiriq
            JwtSecurityTokenHandler tokenHandler = new();
            tokenDto.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenDto.RefreshToken = CreateRefreshToken();

            return tokenDto;


        }

        public string CreateRefreshToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:RefreshTokenSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(refreshToken);
        }
    }
}
