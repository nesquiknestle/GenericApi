using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomNumbersController : ControllerBase
    {
        private readonly IGenericRepository<RoomNumber> _repository;

        public RoomNumbersController(IGenericRepository<RoomNumber> context)
        {
            _repository = context;
        }

        // GET: api/RoomNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomNumber>>> GetRoomNumbers()
        {
            var roomNumber = await _repository.GetAll();
            return Ok(roomNumber);
        }

        // GET: api/RoomNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomNumber>> GetRoomNumber(int id)
        {
            var roomNumber = await _repository.GetById(id);

            if (roomNumber == null)
            {
                return NotFound();
            }

            return roomNumber;
        }

        // PUT: api/RoomNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomNumber(int id, RoomNumber roomNumber)
        {
            if (id != roomNumber.IdNumber)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(roomNumber);

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

        // POST: api/RoomNumbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomNumber>> PostRoomNumber(RoomNumber roomNumber)
        {
            await _repository.Add(roomNumber);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetRoomNumber", new { id = roomNumber.IdNumber }, roomNumber);
        }

        // DELETE: api/RoomNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomNumber(int id)
        {
            var roomNumber = await _repository.GetById(id);
            if (roomNumber == null)
            {
                return NotFound();
            }

            _repository.Delete(roomNumber);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
