using System.Text.Json.Serialization;

namespace Fitness_Tracker.DTOs
{
    public class DietDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Meal { get; set; }
        public int Amount { get; set; }
        public decimal Calories { get; set; }
        [JsonIgnore]
        public decimal TotalCalories { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
