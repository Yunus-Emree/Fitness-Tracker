using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using System.Linq.Expressions;

namespace Fitness_Tracker.Interfaces
{
    public interface IDietService
    {
        Task<List<DietDto>> GetDietListAsync(int userId);
        Task<DietDto> GetDietAsync(int id, int userId);
        Task<DietDto> AddDietAsync(DietDto dietDto);
        Task<bool> DeleteDietAsync(int id, int userId);
        Task<DietDto> UpdateDietAsync(DietDto dietDto);
    }
}
