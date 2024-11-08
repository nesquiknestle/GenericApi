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
    public class DepartamentsController : ControllerBase
    {
        private readonly IGenericRepository<Departament> _repository;

        public DepartamentsController(IGenericRepository<Departament> repository)
        {
            _repository = repository;
        }

        // GET: api/Departaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departament>>> GetDepartaments()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/Departaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departament>> GetDepartament(int id)
        {
            var departament = await _repository.GetById(id);

            if (departament == null)
            {
                return NotFound();
            }

            return departament;
        }

        // PUT: api/Departaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartament(int id, Departament departament)
        {
            if (id != departament.IdDepartament)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(departament);

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

        // POST: api/Departaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departament>> PostDepartament(Departament departament)
        {
            await _repository.Add(departament);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetDepartament", new { id = departament.IdDepartament }, departament);
        }

        // DELETE: api/Departaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartament(int id)
        {
            var departament = await _repository.GetById(id);
            if (departament == null)
            {
                return NotFound();
            }

            _repository.Delete(departament);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
