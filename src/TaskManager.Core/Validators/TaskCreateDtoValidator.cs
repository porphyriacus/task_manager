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
        // _boardRepository = boardRepository;
        public TaskCreateDtoValidator() {
            // will be implemented later
            /*
             
            RuleFor(c => c.Name)
                .MustAsync(IsTaskOnBoardNotExist)
                .WithMessage("Task with this name has already exist on board");
            RuleFor(c => c.BoardId)
                .MustAsync(IsBoardExist)
                .WithMessage("Task with this name has already exist on board");
             
             */

            RuleFor(c => c.Deadline)
                .Must(deadline => deadline == null || deadline > DateTime.UtcNow)
                .WithMessage("Deadline must be in the future");

            RuleFor(x => x.Priority)
                .IsInEnum()
                .When(x => x.Priority.HasValue)
                .WithMessage("Priopity only can be: Low, Medium, High, Critical (or from 0 to 3)");

        }
        private async Task<bool> IsBoardExist(int BoardId, CancellationToken cancellationToken) {
            //not implemented yet
            /*
             var = await 
             */
            return true;
        }

        private async Task<bool> IsTaskOnBoardNotExist(string name, CancellationToken cancellationToken)
        {
            //not implemented yet
            /*
             var = await 
             */
            return true;
        }
    }
}
