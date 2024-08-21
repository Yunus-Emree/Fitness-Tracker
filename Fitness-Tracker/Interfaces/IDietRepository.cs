using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using System.Linq.Expressions;

namespace Fitness_Tracker.Interfaces
{
    public interface IDietRepository
    {
        Task<List<Diet>> GetDietListAsync(int userId);
        Task<Diet> AddAsync(Diet diet);
        Task<bool> DeleteAsync(int id, int userId);
        Task<Diet> UpdateAsync(Diet diet);
        Task<Diet> GetDietAsync(int id, int userId);
    }
}
