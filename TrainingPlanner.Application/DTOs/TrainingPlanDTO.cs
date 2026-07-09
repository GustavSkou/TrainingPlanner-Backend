using System;

namespace TrainingPlanner.Application.DTOs
{
    public class TrainingPlanDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
