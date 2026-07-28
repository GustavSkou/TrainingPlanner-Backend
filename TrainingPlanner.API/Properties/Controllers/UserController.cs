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
        public ActionResult<IEnumerable<object>> GetUser([FromQuery] int[] ids)
        {
            string[] users = ["user0", "user1"];
            if (ids == null || ids.Length == 0)
                return users;
            
            return Ok(ids.Select(id => users[id % users.Length]));
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
