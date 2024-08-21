using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using System.Linq.Expressions;

namespace Fitness_Tracker.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseDto>> GetExerciseListAsync(int userId);
        Task<ExerciseDto> GetExercisesByIdAsync(int id, int userId);
        Task<ExerciseDto> AddExercisesAsync(ExerciseDto exerciseDto);
        Task<bool> DeleteExerciseAsync(int id, int userId);
        Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto exerciseDto);
    }
}
