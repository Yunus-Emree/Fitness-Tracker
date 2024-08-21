namespace Fitness_Tracker.Entities
{
    public class Diet
    {
        public int Id { get; set; }
        public string Meal { get; set; }
        public int Amount { get; set; }
        public decimal Calories { get; set; }
        public decimal TotalCalories { get; set; }
        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
