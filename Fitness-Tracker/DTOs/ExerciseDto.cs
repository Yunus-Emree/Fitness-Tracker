using System.Text.Json.Serialization;

namespace Fitness_Tracker.DTOs
{
    public class ExerciseDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
}
