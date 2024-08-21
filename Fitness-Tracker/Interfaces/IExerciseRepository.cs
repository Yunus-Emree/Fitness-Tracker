using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using System.Linq.Expressions;

namespace Fitness_Tracker.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetExerciseListAsync(int userId);
        Task<Exercise> GetExercisesByIdAsync(int id, int userId);
        Task<Exercise> AddExercisesAsync(Exercise exercise);
        Task<bool> DeleteExerciseAsync(int id, int userId);
        Task<Exercise> UpdateExerciseAsync(Exercise exercise);
    }
}
