using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;
using TaskManager.Core.Interfaces;
using TaskManager.DAL.Interfaces;

namespace TaskManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public Task<TaskResponseDto> CreateAsync(TaskCreateDto create, CancellationToken cancellationToken)
        {

        }
        //— валидация(проверка названия), создание сущности TaskEntity, вызов _taskRepository.AddAsync, затем _context.SaveChangesAsync(). Возвращаешь TaskResponseDto.
        public Task<List<TaskResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<List<TaskResponseDto>> GetListAsync(TaskFilteredDto search, CancellationToken cancellationToken);

        public Task<TaskResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<TaskResponseDto> UpdateAsync(TaskUpdateDto create, CancellationToken cancellationToken);
        public Task<TaskResponseDto> DeleteAsync(int id, CancellationToken cancellationToken);
        //GetAllAsync 
        //— получаешь все задачи из репозитория, преобразуешь в список TaskResponseDto.

        //GetByIdAsync — получаешь задачу, выбрасываешь KeyNotFoundException, если не найдена.

        //UpdateAsync — получаешь задачу, обновляешь поля, вызываешь UpdateAsync и SaveChangesAsync.

        //DeleteAsync — получаешь задачу, вызываешь DeleteAsync и SaveChangesAsync.

        public Task<List<TaskResponseDto>> GetTaskByBoardId(TaskFilteredDto search, CancellationToken cancellationToken);

        public Task<List<CommentResponseDto>> GetTaskComments(int id, CancellationToken cancellationToken);
        public Task<List<UserResponseDto>> GetTaskExecuters(int id, CancellationToken cancellationToken);

    }
}
