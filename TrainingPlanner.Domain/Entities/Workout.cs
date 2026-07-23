namespace TrainingPlanner.Domain.Entities;

public class Workout
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TrainingPlanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public int DistanceMeters { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public TrainingPlan TrainingPlan { get; set; } = null!;
    // Structured workout plan: segments containing intervals (e.g. 5x1km)
    public ICollection<Segment> Segments { get; set; } = new List<Segment>();
}
