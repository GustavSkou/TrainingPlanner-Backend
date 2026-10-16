using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Data;

namespace TrainingPlanner.API.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WorkoutController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] WorkoutDTO dto)
    {
        var workout = ToEntity(dto);
        await _context.Workouts.AddAsync(workout);
        await _context.SaveChangesAsync();
        return Ok(workout);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] WorkoutDTO dto)
    {
        var workout = await _context.Workouts
            .Include(w => w.Segments)
            .ThenInclude(s => s.Intervals)
            .FirstOrDefaultAsync(w => w.Id == id);
        if (workout == null)
            return NotFound();

        workout.UserId = dto.UserId;
        workout.TrainingPlanId = dto.TrainingPlanId;
        workout.Name = dto.Name;
        workout.Description = dto.Description;
        workout.DurationMinutes = dto.DurationMinutes;
        workout.DistanceMeters = dto.DistanceMeters;
        workout.Pace = dto.Pace;
        workout.PaceUnit = dto.PaceUnit;
        workout.Notes = dto.Notes;
        workout.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return Ok(workout);
    }

    private static Workout ToEntity(WorkoutDTO dto)
    {
        var workout = new Workout
        {
            UserId = dto.UserId,
            TrainingPlanId = dto.TrainingPlanId,
            Name = dto.Name,
            Description = dto.Description,
            DurationMinutes = dto.DurationMinutes,
            DistanceMeters = dto.DistanceMeters,
            Pace = dto.Pace,
            PaceUnit = dto.PaceUnit,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var segmentDto in dto.Segments)
        {
            var segment = new Segment
            {
                Workout = workout,
                Order = segmentDto.Order,
                RepeatCount = segmentDto.RepeatCount,
                Notes = segmentDto.Notes
            };
            foreach (var intervalDto in segmentDto.Intervals)
            {
                segment.Intervals.Add(new Interval
                {
                    Segment = segment,
                    Order = intervalDto.Order,
                    DistanceMeters = intervalDto.DistanceMeters,
                    DurationSeconds = intervalDto.DurationSeconds,
                    TargetPaceSecondsPerKm = intervalDto.TargetPaceSecondsPerKm,
                    TargetPaceSecondsPerKmUpperBound = intervalDto.TargetPaceSecondsPerKmUpperBound,
                    TargetPaceSecondsPerKmLowerBound = intervalDto.TargetPaceSecondsPerKmLowerBound,
                    Notes = intervalDto.Notes
                });
            }
            workout.Segments.Add(segment);
        }

        return workout;
    }
}
