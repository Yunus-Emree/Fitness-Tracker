using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new AppUser { FirstName= dto.FirstName, LastName= dto.LastName, UserName= dto.UserName, Email= dto.Email, BirthDate = dto.BirthDate, Gender = dto.Gender, Height = dto.Height, Weight = dto.Weight};
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return Ok(new {result = "User Created Successfully"});
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);
                var token = _authService.GenerateToken(user);
                var name = "Hoşgeldiniz " + user.FirstName + " " + user.LastName;
                var values = "Boy: " + user.Height + " Kilo: " + user.Weight;
                return Ok(new { token ,name, values });
            }
            return Unauthorized();
        }
    }
}
