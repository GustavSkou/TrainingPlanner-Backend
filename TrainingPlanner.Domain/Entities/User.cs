namespace TrainingPlanner.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string LoginProvider { get; set; } = string.Empty;   // fx github
    public string NameIdentifier { get; set; } = string.Empty;  // identifieren fra loginprovider


    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<TrainingPlan> TrainingPlans { get; set; } = new List<TrainingPlan>();
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}
