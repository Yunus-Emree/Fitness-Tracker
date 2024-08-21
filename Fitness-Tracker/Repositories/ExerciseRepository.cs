using Fitness_Tracker.Data;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fitness_Tracker.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly FitnessTrackerDbContext _context;

        public ExerciseRepository(FitnessTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Exercise> AddExercisesAsync(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        public async Task<bool> DeleteExerciseAsync(int id, int userId)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (exercise == null)
            {
                return false;
            }
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Exercise>> GetExerciseListAsync(int userId)
        {
            return await _context.Exercises.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Exercise> GetExercisesByIdAsync(int id, int userId)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<Exercise> UpdateExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }
    }
}
