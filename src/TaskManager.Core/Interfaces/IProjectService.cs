using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.BoardDTO;
using TaskManager.Core.DTOs.ProjectDTO;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Interfaces
{
    public interface IProjectService
    {

        public Task<List<ProjectResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<ProjectResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        public Task<ProjectResponseDto> CreateAsync(ProjectCreateDto create, string ownerId, CancellationToken cancellationToken);
        public Task<ProjectResponseDto> UpdateAsync(ProjectUpdateDto create, CancellationToken cancellationToken);
        public Task<ProjectResponseDto> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
