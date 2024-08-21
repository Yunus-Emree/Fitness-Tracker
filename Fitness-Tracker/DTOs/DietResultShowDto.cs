using System.Text.Json.Serialization;

namespace Fitness_Tracker.DTOs
{
    public class DietResultShowDto
    {
        public int Id { get; set; }
        public string Meal { get; set; }
        public int Amount { get; set; }
        public decimal Calories { get; set; }
        public decimal TotalCalories { get; set; }
        public int UserId { get; set; }
    }
}
