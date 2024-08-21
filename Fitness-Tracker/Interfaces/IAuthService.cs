using Fitness_Tracker.Entities;
using System.Security.Claims;

namespace Fitness_Tracker.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(AppUser user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
