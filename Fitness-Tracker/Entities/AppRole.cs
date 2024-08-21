using Microsoft.AspNetCore.Identity;

namespace Fitness_Tracker.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
