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
    public class TypeOfServicesController : ControllerBase
    {
        private readonly IGenericRepository<TypeOfService> _repository;

        public TypeOfServicesController(IGenericRepository<TypeOfService> repository)
        {
            _repository = repository;
        }

        // GET: api/TypeOfServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfService>>> GetTypeOfServices()
        {
            var typeOfService = await _repository.GetAll();
            return Ok(typeOfService);
        }

        // GET: api/TypeOfServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfService>> GetTypeOfService(int id)
        {
            var typeOfService = await _repository.GetById(id);

            if (typeOfService == null)
            {
                return NotFound();
            }

            return typeOfService;
        }

        // PUT: api/TypeOfServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfService(int id, TypeOfService typeOfService)
        {
            if (id != typeOfService.IdTypeOfServices)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(typeOfService);

                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingType = await _repository.GetById(id);
                if (existingType == null)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return NoContent();
        }

        // POST: api/TypeOfServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfService>> PostTypeOfService(TypeOfService typeOfService)
        {
            await _repository.Add(typeOfService);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfService", new { id = typeOfService.IdTypeOfServices }, typeOfService);
        }

        // DELETE: api/TypeOfServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfService(int id)
        {
            var typeOfService = await _repository.GetById(id);
            if (typeOfService == null)
            {
                return NotFound();
            }

            _repository.Delete(typeOfService);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
