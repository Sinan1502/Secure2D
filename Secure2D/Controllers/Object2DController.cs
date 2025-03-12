using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secure2D.Models;
using Secure2D.Repositories;

namespace Secure2D.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class Object2DController : ControllerBase
    {
        private readonly IObject2DRepository _repository;

        public Object2DController(IObject2DRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object2D>>> Get()
        {
            var objects = await _repository.GetAllAsync();
            return Ok(objects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object2D>> GetById(Guid id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        [HttpGet("environment/{environmentId}")]
        public async Task<ActionResult<IEnumerable<Object2D>>> GetByEnvironmentId(Guid environmentId)
        {
            var objects = await _repository.GetByEnvironmentIdAsync(environmentId);
            return Ok(objects);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Object2D obj)
        {
            if (obj == null) return BadRequest();
            await _repository.AddAsync(obj);
            return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Object2D obj)
        {
            if (obj == null || id != obj.Id) return BadRequest();

            var existingObject = await _repository.GetByIdAsync(id);
            if (existingObject == null) return NotFound();

            await _repository.UpdateAsync(obj);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
