namespace TrainingPlanner.Domain.Entities;

public class TrainingPlan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TrainingTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public TrainingType TrainingType { get; set; } = null!;
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}
