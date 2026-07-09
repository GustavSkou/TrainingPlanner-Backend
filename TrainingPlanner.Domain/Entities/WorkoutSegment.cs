namespace TrainingPlanner.Domain.Entities;

public class WorkoutSegment
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    
    public int Order { get; set; } // Order of this segment within the workout
    
    public int RepeatCount { get; set; } = 1; // How many times the segment repeats, repeating the intervals within.
    
    //public int? RestSeconds { get; set; } // re
    public string? Notes { get; set; }

    // Navigation
    public Workout Workout { get; set; } = null!;
    public ICollection<WorkoutInterval> Intervals { get; set; } = new List<WorkoutInterval>();
}

/* 

segment {


}


*/