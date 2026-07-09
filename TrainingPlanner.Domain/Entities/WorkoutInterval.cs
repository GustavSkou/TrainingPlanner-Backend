namespace TrainingPlanner.Domain.Entities;

public class WorkoutInterval
{
    public int Id { get; set; }
    public int SegmentId { get; set; }
    public int Order { get; set; } // Order within the segment
    public int? DistanceMeters { get; set; }
    public int? DurationSeconds { get; set; }
    
    public int? TargetPaceSecondsPerKm { get; set; }            // the precise pace to run at 
    public int? TargetPaceSecondsPerKmUpperBound { get; set; }  // The slowest pace to run the interval at
    public int? TargetPaceSecondsPerKmLowerBound { get; set; }  // The fastest pace to run the interval at

    public string? Notes { get; set; }

    // Navigation
    public WorkoutSegment Segment { get; set; } = null!;
}
