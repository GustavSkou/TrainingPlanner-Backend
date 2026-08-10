using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.API
{
    [ApiController]
    [Authorize]
    [Route("types")]
    public class TypeController : ControllerBase
    {
        ITrainingTypeService _trainingTypeService;
        public TypeController(ITrainingTypeService trainingTypeService)
        {
            _trainingTypeService = trainingTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTypes()
        {
            return Ok(await _trainingTypeService.GetTypes());
        }
    }
}
