using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.API
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser([FromQuery] int[]? ids, [FromQuery] string? email)
        {
            if (!string.IsNullOrWhiteSpace(email))
                return Ok(await _userService.GetUserByEmail(email));

            if (ids == null || ids.Length == 0)
                return Ok(await _userService.GetUsers());

            return Ok(await _userService.GetUsers());
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO dto)
        {
            Console.WriteLine(dto.ToString());
            try {
                User user = await _userService.CreateUserAsync(dto);
                
                return CreatedAtAction(
                    nameof(GetUser), 
                    new { id = user.Id }, 
                    user
                );    
            } 
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
