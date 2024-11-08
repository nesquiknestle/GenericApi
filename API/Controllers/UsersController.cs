using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository<User> _repository;

        public UsersController(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(user);

                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingUser = await _repository.GetById(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var existingUser = await _repository.GetAll();
            if (existingUser.Any(u => u.Login == user.Login))
            {
                return Conflict("User exists!"); 
            }

            await _repository.Add(user);  
            await _repository.SaveChangesAsync(); 

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                return NotFound();  
            }

            _repository.Delete(user);  
            await _repository.SaveChangesAsync();  

            return NoContent(); 
        }

        private async Task<bool> IsUserExistsAsync(int id)
        {
            return await _repository.GetById(id) != null;
        }
    }
}
