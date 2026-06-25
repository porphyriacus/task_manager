using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Validators
{
    public class TaskSummaryDtoValidator : AbstractValidator<TaskSummaryDto>
    {
        public TaskSummaryDtoValidator() {

            RuleFor(t => t.Name)
                .NotNull()
                .WithMessage("Name is required")
                .Must(n => n?.Length > 2 && n.Length < 201)
                .WithMessage("Can not be longer than 100 symbols and shorter than 3 symbols");

            RuleFor(t => t.Boardname)
               .NotNull()
               .WithMessage("Boardname is required")
               .Must(n => n?.Length > 2 && n.Length < 201)
               .WithMessage("Can not be longer than 100 symbols and shorter than 3 symbols");

            RuleFor(t => t.Description)
                .Must(n => System.String.IsNullOrEmpty(n) || ( n.Length > 2 && n.Length < 1001))
                .WithMessage("Can not be longer than 1000 symbols");

            RuleFor(t => t.OwnerName)
                .Must(n => System.String.IsNullOrEmpty(n) || (n.Length > 2 && n.Length < 101))
                .WithMessage("Can not be longer than 100 symbols");

            /// can be change if there is specific rules
            RuleFor(c => c.Deadline)
               .Must(deadline => deadline == null || deadline > DateTime.UtcNow)
               .WithMessage("Deadline must be in the future");

            RuleFor(x => x.Priority)
                .Must(priority => (System.String.IsNullOrEmpty(priority)) 
                                    || (priority?.ToLower() == "low") 
                                    || (priority?.ToLower() == "medium") 
                                    || (priority?.ToLower() == "hight")
                                    || (priority?.ToLower() == "critical"))
                .WithMessage("Priopity only can be: Low, Medium, High, Critical");

        }
    }
}
