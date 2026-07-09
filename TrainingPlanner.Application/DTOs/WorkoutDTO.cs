using System;

namespace TrainingPlanner.Application.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingPlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public int? CaloriesBurned { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
