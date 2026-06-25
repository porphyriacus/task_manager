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
            
            var boardExists = await _boardRepository.ExistsAsync(dto.BoardId, cancellationToken);
            if (!boardExists)
                throw new ArgumentException($"Board with ID {dto.BoardId} does not exist");

            
            var duplicateExists = await _taskRepository.ExistsAsync(
                t => t.Name == dto.Name && t.BoardId == dto.BoardId,
                cancellationToken
            );

            if (duplicateExists)
                throw new InvalidOperationException($"Task '{dto.Name}' already exists in this board");
             var = await 
             */
            return true;
        }

    }
}
