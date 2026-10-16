using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Data;

namespace TrainingPlanner.API.Controllers;

[ApiController]
[Route("intervals")]
public class IntervalController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IntervalController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] IntervalDTO dto)
    {
        var interval = ToEntity(dto);
        await _context.WorkoutIntervals.AddAsync(interval);
        await _context.SaveChangesAsync();
        return Ok(interval);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] IntervalDTO dto)
    {
        var interval = await _context.WorkoutIntervals.FindAsync(id);
        if (interval == null)
            return NotFound();

        interval.SegmentId = dto.SegmentId;
        interval.Order = dto.Order;
        interval.DistanceMeters = dto.DistanceMeters;
        interval.DurationSeconds = dto.DurationSeconds;
        interval.TargetPaceSecondsPerKm = dto.TargetPaceSecondsPerKm;
        interval.TargetPaceSecondsPerKmUpperBound = dto.TargetPaceSecondsPerKmUpperBound;
        interval.TargetPaceSecondsPerKmLowerBound = dto.TargetPaceSecondsPerKmLowerBound;
        interval.Notes = dto.Notes;
        await _context.SaveChangesAsync();
        return Ok(interval);
    }

    private static Interval ToEntity(IntervalDTO dto) => new()
    {
        SegmentId = dto.SegmentId,
        Order = dto.Order,
        DistanceMeters = dto.DistanceMeters,
        DurationSeconds = dto.DurationSeconds,
        TargetPaceSecondsPerKm = dto.TargetPaceSecondsPerKm,
        TargetPaceSecondsPerKmUpperBound = dto.TargetPaceSecondsPerKmUpperBound,
        TargetPaceSecondsPerKmLowerBound = dto.TargetPaceSecondsPerKmLowerBound,
        Notes = dto.Notes
    };
}
