using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.ProjectDTO;
using TaskManager.Core.Interfaces;
using TaskManager.DAL.Data;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Interfaces;
using TaskManager.DAL.Repositories;

namespace TaskManager.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IProjectRepository _projectRepository;
        public ProjectService(AppDbContext appDbContext
                              , IProjectRepository projectRepository)
        {

            _context = appDbContext;
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(null, cancellationToken
                                                        , projects => projects.Owner);
            return projects.Select(MapToResponseDto).ToList();

        }
        public async Task<ProjectResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id, cancellationToken
                                            ,  t => t.Owner);
                if (project == null)
                {
                    throw new KeyNotFoundException($"Project with ID {id} not found");
                }

                return MapToResponseDto(project);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }
        }

        public async Task<ProjectResponseDto> CreateAsync(ProjectCreateDto create, string ownerId, CancellationToken cancellationToken)
        {
            var exist = await _context.Projects.FirstOrDefaultAsync(b => b.Name == create.Name && b.OwnerId == ownerId, cancellationToken);
            if (exist != null)
            {
                throw new InvalidOperationException("Project with this name already exists");
            }
            var project = new Project(ownerId, create.Name, create.Description);
        
            await _projectRepository.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(project);
        }
        public async Task<ProjectResponseDto> UpdateAsync(ProjectUpdateDto dto, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetByIdAsync(dto.Id, cancellationToken);
            if(project == null) {
                throw new KeyNotFoundException($"Project with ID {dto.Id} not found");
            }
            project.ChangeName(dto.Name);        
            project.ChangeDescription(dto.Description);

            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(project);

        }
        public async Task<ProjectResponseDto> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetByIdAsync(id, cancellationToken);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }

            await _projectRepository.DeleteAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(project);
        }


        private static ProjectResponseDto MapToResponseDto(Project project)
        {
            return new ProjectResponseDto
            {
                Id = project.Id,

                OwnerId = project.OwnerId,
                OwnerName = project.Owner?.UserName ?? "Unknown",

                Name = project.Name,
                Description = project.Description,

                CreatedAt = project.CreatedAt
            };
        }
    }
}
