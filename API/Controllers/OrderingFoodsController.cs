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
    public class OrderingFoodsController : ControllerBase
    {
        private readonly IGenericRepository<OrderingFood> _repository;

        public OrderingFoodsController(IGenericRepository<OrderingFood> repository)
        {
            _repository = repository;
        }

        // GET: api/OrderingFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderingFood>>> GetOrderingFoods()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/OrderingFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderingFood>> GetOrderingFood(int id)
        {
            var orderingFood = await _repository.GetById(id);

            if (orderingFood == null)
            {
                return NotFound();
            }

            return orderingFood;
        }

        // PUT: api/OrderingFoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderingFood(int id, OrderingFood orderingFood)
        {
            if (id != orderingFood.IdOrderingFood)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(orderingFood);

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

        // POST: api/OrderingFoods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderingFood>> PostOrderingFood(OrderingFood orderingFood)
        {
            await _repository.Add(orderingFood);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetOrderingFood", new { id = orderingFood.IdOrderingFood }, orderingFood);
        }

        // DELETE: api/OrderingFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderingFood(int id)
        {
            var orderingFood = await _repository.GetById(id);
            if (orderingFood == null)
            {
                return NotFound();
            }

            _repository.Delete(orderingFood);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
