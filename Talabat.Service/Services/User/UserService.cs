using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
           // 1. check Email Existed in Database
           
           var user =  await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                return null;

            // 2.Check Password
          var result =  await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return null;
            // 3. Return UserDto[DisplayName - Email - Token]
            return new UserDto() 
            {
                DisplayName = user.DisplayName,
                Email= user.Email,
                Token = await _tokenService.CreateTokenAsync(user,_userManager)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto resgisterDto)
        {
            // 1. Check if the entered email is already exixts

            if (await CheckEmailExist(resgisterDto.Email))
                return null;

            var user = new AppUser()
            {
                Email = resgisterDto.Email,
                DisplayName = resgisterDto.DisplayName,
                PhoneNumber = resgisterDto.PhoneNumber,
                UserName = resgisterDto.Email.Split('@')[0],
            };

            var result = await _userManager.CreateAsync(user, resgisterDto.Password);

            if (!result.Succeeded)
                return null;

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }


        public async Task<bool> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null; // If return true so Email is already Exist
        }
    }
}
