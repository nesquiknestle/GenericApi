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
    public class PostsController : ControllerBase
    {
        private readonly IGenericRepository<Post> _repository;

        public PostsController(IGenericRepository<Post> repository)
        {
            _repository = repository;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return Ok(await _repository.GetAll());
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _repository.GetById(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.IdPost)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(post);

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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await _repository.Add(post);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.IdPost }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _repository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            _repository.Delete(post);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
