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
    public class BillForServicesController : ControllerBase
    {
        private readonly IGenericRepository<BillForService> _repository;

        public BillForServicesController(IGenericRepository<BillForService> repository)
        {
            _repository = repository;
        }

        // GET: api/BillForServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillForService>>> GetBillForServices()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/BillForServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillForService>> GetBillForService(int id)
        {
            var billForService = await _repository.GetById(id);

            if (billForService == null)
            {
                return NotFound();
            }

            return billForService;
        }

        // PUT: api/BillForServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillForService(int id, BillForService billForService)
        {
            if (id != billForService.IdBillForServices)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(billForService);

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

        // POST: api/BillForServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillForService>> PostBillForService(BillForService billForService)
        {
            await _repository.Add(billForService);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetBillForService", new { id = billForService.IdBillForServices }, billForService);
        }

        // DELETE: api/BillForServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillForService(int id)
        {
            var billForService = await _repository.GetById(id);
            if (billForService == null)
            {
                return NotFound();
            }

            _repository.Delete(billForService);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
