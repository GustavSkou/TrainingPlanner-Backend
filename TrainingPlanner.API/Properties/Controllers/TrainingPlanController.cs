using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.API.Controllers;

[ApiController]
[Authorize]
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
    public async Task<ActionResult> GetTrainingPlansById([FromQuery] int id)
    {
        var trainingPlan = await _trainingPlanService.GetPlanById(id);
        return Ok(trainingPlan);
    }

    [HttpGet("user")]
    public async Task<ActionResult> GetTrainingPlansByUserId([FromQuery] int id)
    {
        IEnumerable<TrainingPlan> trainingPlans = await _trainingPlanService.GetPlansByUserId(id);
        return Ok(trainingPlans);
    }

    [HttpGet("TEST")]
    public ActionResult<object> GetTest()
    {
        return "test";
    }
}
