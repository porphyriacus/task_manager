using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;
/*

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskPriority Priority { get; set; }
    }
 
 */
namespace TaskManager.Core.Validators
{
    public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateDtoValidator() {
            RuleFor(c => c.Deadline)
              .Must(deadline => deadline == null || deadline > DateTime.UtcNow)
              .WithMessage("Deadline must be in the future");

            RuleFor(x => x.Priority)
                .IsInEnum()
                .When(x => x.Priority.HasValue)
                .WithMessage("Priopity only can be: Low, Medium, High, Critical (or from 0 to 3)");
        }
    }
}
