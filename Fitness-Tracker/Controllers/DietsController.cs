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
    public class DietsController : ControllerBase
    {
        private readonly IDietService _dietService;
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public DietsController(IDietService dietService, UserManager<AppUser> userManager, IAuthService authService)
        {
            _dietService = dietService;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDietList([FromQuery] string token)
        {
            var claimsPrincipal = _authService.ValidateToken(token);
            if (claimsPrincipal == null)
            {
                return Unauthorized("Invalid Token");
            }
            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid Token");
            }
            var userId = int.Parse(userIdClaim.Value);
            var diets = await _dietService.GetDietListAsync(userId);

            var dietList = new List<DietResultShowDto>();

            foreach (var diet in diets)
            {
                DietResultShowDto dietResultShowDto = new DietResultShowDto()
                {
                    Id = diet.Id,
                    Meal = diet.Meal,
                    Amount = diet.Amount,
                    Calories = diet.Calories,
                    TotalCalories = diet.TotalCalories,
                    UserId = diet.UserId
                };
                dietList.Add(dietResultShowDto);
            }

            return Ok(dietList);
        }
        [HttpPost]
        public async Task<IActionResult> AddDiet([FromBody] DietDto dietDto)
        {
            var totalCalories = dietDto.Amount * dietDto.Calories;
            dietDto.TotalCalories = totalCalories;

            var userId = _userManager.GetUserId(User);
            dietDto.UserId = Convert.ToInt32(userId);
            var diet = await _dietService.AddDietAsync(dietDto);

            DietResultShowDto dietResultShowDto = new DietResultShowDto
            {
                Id = diet.Id,
                Meal = dietDto.Meal,
                Amount = dietDto.Amount,
                Calories = dietDto.Calories,
                TotalCalories = dietDto.TotalCalories,
                UserId = dietDto.UserId
            };

            return CreatedAtAction(nameof(AddDiet), new { id = diet.Id }, dietResultShowDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiet(int id)
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var diet = await _dietService.GetDietAsync(id, userId);
            if (diet == null)
            {
                return NotFound();
            }
            DietResultShowDto dietResultShowDto = new DietResultShowDto
            {
                Id = diet.Id,
                Meal = diet.Meal,
                Amount = diet.Amount,
                Calories = diet.Calories,
                TotalCalories = diet.TotalCalories,
                UserId = diet.UserId
            };
            return Ok(dietResultShowDto);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(int id, [FromBody] DietDto dietDto)
        {
            var totalCalories = dietDto.Amount * dietDto.Calories;
            dietDto.TotalCalories = totalCalories;

            dietDto.Id = id;
            
            var userId = _userManager.GetUserId(User);
            dietDto.UserId = Convert.ToInt32(userId);
            var updatedDiet = await _dietService.UpdateDietAsync(dietDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var deletedDiet = await _dietService.DeleteDietAsync(id, userId);
            if (!deletedDiet)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
