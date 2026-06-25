using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Interfaces
{
    public interface ITaskService
    {
        public Task<TaskSummaryDto> CreateAsync(TaskCreateDto create, string ownerId, CancellationToken cancellationToken);
        public Task<List<TaskSummaryDto>> GetListAsync(TaskFilteredDto search, CancellationToken cancellationToken);

        public Task<TaskSummaryDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<TaskSummaryDto> UpdateAsync(TaskUpdateDto create, CancellationToken cancellationToken);
        public Task<TaskSummaryDto> DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<List<TaskSummaryDto>> GetTaskByBoardId(int boardId, CancellationToken cancellationToken);

    }
}
