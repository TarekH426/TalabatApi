using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service.Contract;
using Talabat.Service.Services.Token;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
   
    public class AccountController : BaseApiController // api/Account
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }


        [HttpPost("login")] // api/Account/login
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto) 
        {
           var userDto = await _userService.LoginAsync(loginDto);

            if (userDto is null)
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
            }
            return Ok(userDto);
        }


        [HttpPost("register")] // api/account/register

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await _userService.RegisterAsync(registerDto);

            if(userDto is null)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

            }
            return Ok(userDto);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<UserDto> GetCurrentUserDetails()
        {
            var userId = User?.FindFirst("UserId");

            var user = await _userManager.FindByIdAsync(userId.Value);

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
               
            };
        }

    }
}
