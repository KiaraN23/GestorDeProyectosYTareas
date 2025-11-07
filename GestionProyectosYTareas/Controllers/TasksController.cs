using GestionProyectosYTareas.Shared.DTOs;
using GestionProyectosYTareas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionProyectosYTareas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemService _service;

        public TasksController(ITaskItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? projectId = null, [FromQuery] string? status = null, [FromQuery] string? q = null, [FromQuery] string? sort = null)
        {
            var result = await _service.GetPagedAsync(page, pageSize, projectId, status, q, sort);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskItemUpdateDto dto, [FromHeader(Name = "If-Match")] string? rowVersionBase64)
        {
            byte[]? rowVersion = rowVersionBase64 != null ? Convert.FromBase64String(rowVersionBase64) : null;
            try
            {
                var updated = await _service.UpdateAsync(id, dto, rowVersion);
                return updated ? NoContent() : NotFound();
            }
            catch (InvalidOperationException)
            {
                return Conflict(new { message = "Error de concurrencia" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
