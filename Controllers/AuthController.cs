using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskFlow_API.Entities;
using TaskFlow_API.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TaskFlow_API.Services;
using Microsoft.AspNetCore.Authorization;

namespace TaskFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserDto request)
        {
            var user = await authService.RegisterAsync (request);
            if (user == null)
            {
                return BadRequest("Username already exists.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync (request);
            if (result == null)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public  async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result  = await authService.RefreshTokenAsync (request);
            if(result is null|| result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh Token");
            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an Admin!");
        }
    }
}
