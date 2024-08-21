using Fitness_Tracker.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data
{
    public class FitnessTrackerDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options) : base(options) { }
        
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Diet> Diets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.Property(e => e.Calories).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalCalories).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Weight).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Height).HasColumnType("decimal(18,2)");
            });
        }
    }
}
