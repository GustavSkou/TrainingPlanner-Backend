using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.API.Controllers;

[ApiController]
[Route("plans")]
public class TrainingPlanController : ControllerBase
{
    private readonly ITrainingPlanService _trainingPlanService;

    public TrainingPlanController(ITrainingPlanService trainingPlanService)
    {
        _trainingPlanService = trainingPlanService;
    }
              
    [HttpPost("create")]
    public async Task<ActionResult> CreateTrainingPlan([FromBody] TrainingPlanDTO dto)
    {
        Console.WriteLine(dto.ToString());
        try
        {
            TrainingPlan trainingPlan = await _trainingPlanService.CreateTrainingPlan(dto);

            return Ok(trainingPlan); /*CreatedAtAction(
                nameof(GetTrainingPlan),
                new { id = trainingPlan.Id },
                trainingPlan
            );*/
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetTrainingPlans([FromQuery] int? id)
    {
        if (id.HasValue)
            return Ok(await _trainingPlanService.GetPlanById(id.Value));

        return Ok(await _trainingPlanService.GetPlans());
    }

    [HttpGet("user")]
    public async Task<ActionResult> GetTrainingPlansByUserId([FromQuery] int userId)
    {
        IEnumerable<TrainingPlan> trainingPlans = await _trainingPlanService.GetPlansByUserId(userId);
        return Ok(trainingPlans);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateTrainingPlan(int id, [FromBody] TrainingPlanDTO dto)
    {
        dto.Id = id;
        try
        {
            return Ok(await _trainingPlanService.UpdateTrainingPlan(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
