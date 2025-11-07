using GestionProyectosYTareas.Application.Services;
using GestionProyectosYTareas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GestionProyectosYTareas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _service;
        public ProjectsController(ProjectService service) => _service = service;


        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] string? sort = null)
        {
            var (items, total) = await _service.GetPagedAsync(page, pageSize, search, sort);
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);


            return Ok(new { totalItems = total, totalPages, page, pageSize, items });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _service.GetByIdAsync(id);
            return project == null ? NotFound() : Ok(project);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name) || project.Name.Length < 3)
                return BadRequest(new { message = "Name must have at least 3 characters" });


            var created = await _service.CreateAsync(project);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Project project)
        {
            var ok = await _service.UpdateAsync(id, project);
            return ok ? NoContent() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
