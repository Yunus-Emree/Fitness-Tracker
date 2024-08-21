using Microsoft.AspNetCore.Identity;

namespace Fitness_Tracker.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Gender { get; set; }
    }
}
