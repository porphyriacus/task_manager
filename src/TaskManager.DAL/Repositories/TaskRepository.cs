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
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TaskEntity> _tasks;
        public TaskRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _tasks = _dbContext.Set<TaskEntity>();
        }

        public async Task<TaskEntity> GetTaskByIdAsync(
           int id
           , CancellationToken cancellationToken = default
           , Expression<Func<TaskEntity, object>>[]? includeProperties = null
           )
        {
            var query = _tasks.AsQueryable();
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    if (property != null)
                        query = query.Include(property);
                }

            }
            var task = await query.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (task != null)
                return task;

            throw new Exception($"Task with ID {id} not found");
        
        }

        public async Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _tasks.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TaskEntity>> ListAsync(
            Func<IQueryable<TaskEntity>, IOrderedQueryable<TaskEntity>>? sortedBy
            , Expression<Func<TaskEntity, bool>>[]? filters = null
            , CancellationToken cancellationToken = default
            , Expression<Func<TaskEntity, object>>[]? includeProperties = null
        )
        {
            var query = _tasks.AsQueryable();
            
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

            if (sortedBy != null)
            {
                query = sortedBy(query);
            }
            else
            {
                query = query.OrderBy(t => t.Id);
            }

            return await query.ToListAsync(cancellationToken);

        }

        public async Task AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            var exists = await _tasks.AnyAsync(t => t.Name == task.Name && t.BoardId == task.BoardId, cancellationToken);
            if (exists)
                throw new InvalidOperationException("Task with this name already exists in the board");

            await _tasks.AddAsync(task, cancellationToken);
        }

        public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            var exist = await _tasks.FirstOrDefaultAsync(t => t.Id == task.Id, cancellationToken);
            if(exist == null)
            {
                throw new InvalidOperationException($"Task with ID {task.Id} not found");
            }
            _dbContext.Entry(exist).CurrentValues.SetValues(task);
        }

        public async Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            var exist = await _tasks.FirstOrDefaultAsync(t => t.Id == task.Id, cancellationToken);
            if (exist == null)
            {
                throw new InvalidOperationException($"Task with ID {task.Id} not found");
            }
            _tasks.Remove(exist);
        }

    }
}
