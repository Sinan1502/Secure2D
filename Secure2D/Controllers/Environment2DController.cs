using Microsoft.AspNetCore.Mvc;
using Secure2D.Models;
using Secure2D.Repositories;

namespace Secure2D.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Environment2DController : ControllerBase
    {
        private readonly IEnvironment2DRepository _repository;

        public Environment2DController(IEnvironment2DRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
        {
            var environments = await _repository.GetAllAsync();
            return Ok(environments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Environment2D>> GetById(Guid id)
        {
            var environment = await _repository.GetByIdAsync(id);
            if (environment == null) return NotFound();
            return Ok(environment);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Environment2D environment)
        {
            if (environment == null) return BadRequest();
            await _repository.AddAsync(environment);
            return CreatedAtAction(nameof(GetById), new { id = environment.Id }, environment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Environment2D environment)
        {
            if (environment == null || id != environment.Id) return BadRequest();

            var existingEnvironment = await _repository.GetByIdAsync(id);
            if (existingEnvironment == null) return NotFound();

            await _repository.UpdateAsync(environment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
