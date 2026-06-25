using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Interfaces
{
    public interface ITaskService
    {
        public Task<TaskSummaryDto> CreateAsync(TaskCreateDto create, CancellationToken cancellationToken);
        //— валидация(проверка названия), создание сущности TaskEntity, вызов _taskRepository.AddAsync, затем _context.SaveChangesAsync(). Возвращаешь TaskResponseDto.
        public Task<List<TaskSummaryDto>> GetListAsync(TaskFilteredDto search, CancellationToken cancellationToken);

        public Task<TaskSummaryDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<TaskSummaryDto> UpdateAsync(TaskUpdateDto create, CancellationToken cancellationToken);
        public Task<TaskSummaryDto> DeleteAsync(int id, CancellationToken cancellationToken);
        //GetAllAsync 
        //— получаешь все задачи из репозитория, преобразуешь в список TaskResponseDto.

        //GetByIdAsync — получаешь задачу, выбрасываешь KeyNotFoundException, если не найдена.

        //UpdateAsync — получаешь задачу, обновляешь поля, вызываешь UpdateAsync и SaveChangesAsync.

        //DeleteAsync — получаешь задачу, вызываешь DeleteAsync и SaveChangesAsync.

        public Task<List<TaskSummaryDto>> GetTaskByBoardId(TaskFilteredDto search, CancellationToken cancellationToken);

    }
}
