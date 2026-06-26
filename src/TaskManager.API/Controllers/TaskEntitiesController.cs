using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskEntitiesController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IValidator<TaskCreateDto> _createValidator;
        private readonly IValidator<TaskUpdateDto> _updateValidator;
        private readonly IValidator<TaskFilteredDto> _filterValidator;

        public TaskEntitiesController(
            ITaskService taskService
            , IValidator<TaskCreateDto> createValidator
            , IValidator<TaskUpdateDto> updateValidator
            , IValidator<TaskFilteredDto> filterValidator)
        {
            _taskService = taskService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _filterValidator = filterValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetAllAsync(cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) {

            try
            {
                var task = await _taskService.GetByIdAsync(id, cancellationToken);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpGet("board/{BoardId}")]
        public async Task<IActionResult> GetByBoardId(int BoardId, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskService.GetTaskByBoardId(BoardId, cancellationToken);
                if(tasks == null || !tasks.Any())
                    return NotFound();
                else 
                    return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] TaskFilteredDto dto, CancellationToken cancellationToken)
        {
            var validationResult = await _filterValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var tasks = await _taskService.GetListAsync(dto, cancellationToken);
            return tasks == null || !tasks.Any() 
                ? NotFound() 
                : Ok(tasks);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto dto, CancellationToken ct)
        {
            var validationResult = await _createValidator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            

            // until BoardRepository added
            dto.BoardId = 1;
            // until JWT added
            var ownerId = "test_user_id";
            //var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (string.IsNullOrEmpty(ownerId))
            //    return Unauthorized();

            var task = await _taskService.CreateAsync(dto, ownerId, ct);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var validationResult = await _updateValidator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var task = await _taskService.UpdateAsync(dto, ct);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                await _taskService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

