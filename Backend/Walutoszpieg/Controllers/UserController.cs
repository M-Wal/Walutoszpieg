using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walutoszpieg.DTO;
using Walutoszpieg.Model;
using Walutoszpieg.Repositories;

namespace Walutoszpieg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _user;

        public UserController(UserRepository UserDto)
        {
            _user = UserDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await _user.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _user.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(User user)
        {
            var id = await _user.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            var result = await _user.UpdateUserAsync(user);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _user.DeleteUserAsync(id);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}
