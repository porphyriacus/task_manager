using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.ProjectDTO;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var projects = await _projectService.GetAllAsync(cancellationToken);
            if (projects != null && projects.Any())
                return Ok(projects);
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(id, cancellationToken);
                return Ok(project);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var ownerId = "test_user_id";
                //var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //if (string.IsNullOrEmpty(ownerId))
                //    return Unauthorized();

                var project = await _projectService.CreateAsync(dto, ownerId, cancellationToken);
                return Ok(project);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ProjectUpdateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectService.UpdateAsync(dto, cancellationToken);
                return Ok(project);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _projectService.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
