using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public ExercisesController(IExerciseService exerciseService, UserManager<AppUser> userManager, IAuthService authService)
        {
            _exerciseService = exerciseService;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises([FromQuery] string token)
        {
            var claimsPrincipal = _authService.ValidateToken(token);
            if (claimsPrincipal == null)
            {
                return Unauthorized("Invalid Token");
            }

            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token.");
            }

            var userId = int.Parse(userIdClaim.Value);
            var exercises = await _exerciseService.GetExerciseListAsync(userId);

            var exerciseList = new List<ExerciseResultShowDto>();

            foreach (var exercise in exercises)
            {
                ExerciseResultShowDto exerciseResultShowDto = new ExerciseResultShowDto()
                {
                    Id = exercise.Id,
                    UserId = exercise.UserId,
                    Name = exercise.Name,
                    Duration = exercise.Duration
                };
                exerciseList.Add(exerciseResultShowDto);
            }
            return Ok(exerciseList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercise(int id)
        {
            var userId = _userManager.GetUserId(User);
            var exercise = await _exerciseService.GetExercisesByIdAsync(id, Convert.ToInt32(userId));
            if (exercise == null)
            {
                return NotFound();
            }
            ExerciseResultShowDto exerciseResultShowDto = new ExerciseResultShowDto()
            {
                Id = exercise.Id,
                UserId = exercise.UserId,
                Name = exercise.Name,
                Duration = exercise.Duration
            };
            return Ok(exerciseResultShowDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] ExerciseDto exerciseDto)
        {
            var userId = _userManager.GetUserId(User);
            exerciseDto.UserId = Convert.ToInt32(userId);
            var createdExercise = await _exerciseService.AddExercisesAsync(exerciseDto);

            ExerciseResultShowDto exerciseResultShowDto = new ExerciseResultShowDto()
            {
                Id = createdExercise.Id,
                UserId = exerciseDto.UserId,
                Name = exerciseDto.Name,
                Duration = exerciseDto.Duration
            };

            return CreatedAtAction(nameof(AddExercise), new { id = createdExercise.Id }, exerciseResultShowDto);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] ExerciseDto exerciseDto)
        {
            exerciseDto.Id = id;
            var userId = _userManager.GetUserId(User);
            exerciseDto.UserId = Convert.ToInt32(userId);
            var updatedExercise = await _exerciseService.UpdateExerciseAsync(exerciseDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var userId = _userManager.GetUserId(User);
            var deletedExercise = await _exerciseService.DeleteExerciseAsync(id, Convert.ToInt32(userId));
            if (!deletedExercise)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
