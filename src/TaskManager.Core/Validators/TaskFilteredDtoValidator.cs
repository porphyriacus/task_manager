using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Validators
{
    public class TaskFilteredDtoValidator : AbstractValidator<TaskFilteredDto>
    {
        public TaskFilteredDtoValidator() {

            RuleFor(d => d.SortedBy)
                .Must(sortedBy  => (System.String.IsNullOrEmpty(sortedBy)) || (sortedBy?.ToLower() == "deadline") || (sortedBy?.ToLower() == "priority") || (sortedBy?.ToLower() == "name"))
                .WithMessage("Sort field must be: \"deadline\", \"priority\" or \"name\" ");
        }
    }
}
