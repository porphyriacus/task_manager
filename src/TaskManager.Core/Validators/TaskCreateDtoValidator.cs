using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;
using TaskManager.DAL.Entities;

namespace TaskManager.Core.Validators
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator() {
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
