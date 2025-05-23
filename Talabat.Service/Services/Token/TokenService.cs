using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service.Contract;
using Microsoft.IdentityModel.Tokens;
namespace Talabat.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // Header 
        // Payload
        // Signature
        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),

            };

            var roles = await userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken
           (
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:audience"],
                claims:authClaims,
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:ExpireTime"])),
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
