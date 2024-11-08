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
    public class OrderingRoomsController : ControllerBase
    {
        private readonly IGenericRepository<OrderingRoom> _repository;

        public OrderingRoomsController(IGenericRepository<OrderingRoom> repository)
        {
            _repository = repository;
        }

        // GET: api/OrderingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderingRoom>>> GetOrderingRooms()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/OrderingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderingRoom>> GetOrderingRoom(int id)
        {
            var orderingRoom = await _repository.GetById(id);

            if (orderingRoom == null)
            {
                return NotFound();
            }

            return orderingRoom;
        }

        // PUT: api/OrderingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderingRoom(int id, OrderingRoom orderingRoom)
        {
            if (id != orderingRoom.IdOrderingRoom)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(orderingRoom);

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

        // POST: api/OrderingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderingRoom>> PostOrderingRoom(OrderingRoom orderingRoom)
        {
            await _repository.Add(orderingRoom);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetOrderingRoom", new { id = orderingRoom.IdOrderingRoom }, orderingRoom);
        }

        // DELETE: api/OrderingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderingRoom(int id)
        {
            var orderingRoom = await _repository.GetById(id);
            if (orderingRoom == null)
            {
                return NotFound();
            }

            _repository.Delete(orderingRoom);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
