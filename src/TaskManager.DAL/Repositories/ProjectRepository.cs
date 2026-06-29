using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.DAL.Data;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Interfaces;

namespace TaskManager.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbSet<Project> _project;
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            
            _context = context;
            _project = _context.Set<Project>();
        }

        public async Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<Project, object>>[]? includeProperties)
        {
            var query = _project.AsQueryable();
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    if (property != null)
                        query = query.Include(property);
                }

            }

            var project = await query.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (_project != null)
                return project;

            throw new KeyNotFoundException($"Project with ID {id} not found");
        }
        public async Task<IReadOnlyCollection<Project>> ListAsync(
            List<Expression<Func<Project, bool>>>? filters = null
            , CancellationToken cancellationToken = default
            , params Expression<Func<Project, object>>[]? includeProperties)
        {
            var query = _project.AsQueryable();
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    if (filter != null)
                        query = query.Where(filter);
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    if (property != null)
                        query = query.Include(property);
                }

            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _project.AddAsync(project, cancellationToken);
        }

        public async Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
        {

            var exist = await _project.FirstOrDefaultAsync(b => b.Id == project.Id, cancellationToken);
            if (exist == null)
            {
                throw new InvalidOperationException($"Project with ID {project.Id} not found");
            }
            _context.Entry(exist).CurrentValues.SetValues(project);
        }

        public async Task DeleteAsync(Project project, CancellationToken cancellationToken = default)
        {
            var exist = await _project.FirstOrDefaultAsync(b => b.Id == project.Id, cancellationToken);
            if (exist == null)
            {
                throw new InvalidOperationException($"Project with ID {project.Id} not found");
            }
            _project.Remove(project);
        }
    }
}
