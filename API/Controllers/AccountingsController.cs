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
    public class AccountingsController : ControllerBase
    {
        private readonly IGenericRepository<Accounting> _repository;

        public AccountingsController(IGenericRepository<Accounting> repository)
        {
            _repository = repository;
        }

        // GET: api/Accountings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accounting>>> GetAccountings()
        {
            var accountings = await _repository.GetAll();
            return Ok(accountings);
        }

        // GET: api/Accountings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accounting>> GetAccounting(int id)
        {
            var accounting = await _repository.GetById(id);

            if (accounting == null)
            {
                return NotFound();
            }

            return accounting;
        }

        // PUT: api/Accountings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccounting(int id, Accounting accounting)
        {
            if (id != accounting.IdAccounting)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(accounting);

                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingAccount = await _repository.GetById(id);
                if (existingAccount == null)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return NoContent();
        }

        // POST: api/Accountings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accounting>> PostAccounting(Accounting accounting)
        {
            await _repository.Add(accounting);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetAccounting", new { id = accounting.IdAccounting }, accounting);
        }

        // DELETE: api/Accountings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounting(int id)
        {
            var account = await _repository.GetById(id);
            if (account == null)
            {
                return NotFound();
            }

            _repository.Delete(account);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
