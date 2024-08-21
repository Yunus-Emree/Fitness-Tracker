namespace Fitness_Tracker.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
