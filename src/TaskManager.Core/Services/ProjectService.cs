using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.ProjectDTO;
using TaskManager.Core.Interfaces;
using TaskManager.DAL.Data;

namespace TaskManager.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IProjectRepository _projectService;
        public ProjectService(AppDbContext appDbContext)
        {

            _context = appDbContext;
        }
        public Task<List<ProjectResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<ProjectResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        public Task<ProjectResponseDto> CreateAsync(ProjectCreateDto create, string ownerId, CancellationToken cancellationToken);
        public Task<ProjectResponseDto> UpdateAsync(ProjectUpdateDto create, CancellationToken cancellationToken);
        public Task<ProjectResponseDto> DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
