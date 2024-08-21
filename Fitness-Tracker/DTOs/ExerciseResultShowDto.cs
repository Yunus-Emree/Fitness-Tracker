using System.Text.Json.Serialization;

namespace Fitness_Tracker.DTOs
{
    public class ExerciseResultShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int UserId { get; set; }
    }
}
