using Fitness_Tracker.Data;
using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Fitness_Tracker.Repositories
{
    public class DietRepository : IDietRepository
    {
        private readonly FitnessTrackerDbContext _context;

        public DietRepository(FitnessTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Diet> AddAsync(Diet diet)
        {
            await _context.Diets.AddAsync(diet);
            await _context.SaveChangesAsync();
            return diet;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var Diet = await _context.Diets.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            if (Diet == null)
            {
                return false;
            }
            _context.Diets.Remove(Diet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Diet> GetDietAsync(int id, int userId)
        {
            var Diet = await _context.Diets.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            return Diet;
        }

        public async Task<List<Diet>> GetDietListAsync(int userId)
        {
            return await _context.Diets.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<Diet> UpdateAsync(Diet diet)
        {
            _context.Diets.Update(diet);
            await _context.SaveChangesAsync();
            return diet;
        }
    }
}
