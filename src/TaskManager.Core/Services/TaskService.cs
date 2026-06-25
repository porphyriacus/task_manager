using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Validators;
using TaskManager.DAL.Data;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Interfaces;

namespace TaskManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        // private readonly IBoardRepository _boardRepository;
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _context = context;
        }


        public async Task<TaskSummaryDto> CreateAsync(TaskCreateDto dto, string ownerId, CancellationToken cancellationToken)
        {
            var priority = dto.Priority ?? TaskPriority.Medium;
            if (dto.Deadline.HasValue && dto.Deadline.Value.Date == DateTime.UtcNow.Date)
                priority = TaskPriority.High;

            var task = new TaskEntity(
                dto.BoardId
                , ownerId
                , dto.Name
                , dto.Description ?? null
                , dto.Deadline
                , priority);

            await _taskRepository.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToSummaryDto(task);
        }

        public async Task<List<TaskSummaryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            
            var tasks = await _taskRepository.GetAllAsync(cancellationToken);
            return tasks.Select(MapToSummaryDto).ToList();
        }


        public async Task<List<TaskSummaryDto>> GetListAsync(TaskFilteredDto search, CancellationToken cancellationToken)
        {
            List<Expression<Func<TaskEntity, bool>>> filter = new List<Expression<Func<TaskEntity, bool>>>();
            if(search.SearchTerm != null)
            {
                filter.Add(t => t.Name.Contains(search.SearchTerm));
            }
            if (search.BoardId != null)
            {
                filter.Add(t => t.BoardId == search.BoardId);
            }

            Func<IQueryable<TaskEntity>, IOrderedQueryable<TaskEntity>>? orderBy = null;
            if(!string.IsNullOrWhiteSpace(search.SortedBy))
            {
                orderBy = search.SortedBy.ToLower() switch
                {
                    "deadline" => search.IsDescending ? 
                                  q => q.OrderByDescending(t => t.Deadline) 
                                  : q => q.OrderBy(t => t.Deadline),
                    "priority" => search.IsDescending ?
                                  q => q.OrderByDescending(t => t.Priority)
                                  : q => q.OrderBy(t => t.Priority),
                    "name" => search.IsDescending ?
                                  q => q.OrderByDescending(t => t.Name)
                                  : q => q.OrderBy(t => t.Name),
                    _ => q => q.OrderBy(t => t.Id)
                };
            }

            var tasks = await _taskRepository.ListAsync(
                orderBy
                , filter
                , cancellationToken
                , t => t.Board
                , t => t.Owner);
            return tasks.Select(MapToSummaryDto).ToList();
        }

        public async Task<TaskSummaryDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id, cancellationToken
                                                        , t => t.Board
                                                        , t => t.Owner);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found");
            }

            return MapToSummaryDto(task);
        }

        public async Task<TaskSummaryDto> UpdateAsync(TaskUpdateDto dto, CancellationToken cancellationToken)
        {
            TaskEntity task = await _taskRepository.GetTaskByIdAsync(dto.Id, cancellationToken);
            if(task == null)
                throw new KeyNotFoundException($"Task with ID {dto.Id} not found");

            task.ChangeName(dto.Name);
            if(!System.String.IsNullOrEmpty(dto.Description))
                task.ChangeDescription(dto.Description);
            if (dto.Priority != null)
                task.ChangePriority((TaskPriority)dto.Priority);
            if(dto.Deadline != null)
                task.ChangeDeadline(dto.Deadline);

            await _taskRepository.UpdateAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return MapToSummaryDto(task);

        }
        public async Task<TaskSummaryDto> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            TaskEntity task = await _taskRepository.GetTaskByIdAsync(id, cancellationToken);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");
            await _taskRepository.DeleteAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return MapToSummaryDto(task);

        }
 

        public async Task<List<TaskSummaryDto>> GetTaskByBoardId(int boardId, CancellationToken cancellationToken)
        {
            List<Expression<Func<TaskEntity, bool>>> filter = new List<Expression<Func<TaskEntity, bool>>> { t => t.BoardId == boardId};

            var tasks = await _taskRepository.ListAsync(
                null
                , filter
                , cancellationToken
                , t => t.Board
                , t => t.Owner);

            return tasks.Select(MapToSummaryDto).ToList();
        }



        private TaskSummaryDto MapToSummaryDto(TaskEntity task)
        {
            return new TaskSummaryDto
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Boardname = task.Board?.Name ?? "Unknown",
                OwnerId = task.OwnerId,
                OwnerName = task.Owner?.UserName ?? "Unknown",
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                Priority = task.Priority.ToString()
            };
        }
    }
}
