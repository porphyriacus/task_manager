using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Interfaces
{
    internal interface ITaskRepository
    {
        public Task<TaskEntity> GetTaskByIdAsync(
            int id
            , CancellationToken cancellationToken = default
            , Expression<Func<TaskEntity, object>>[]? includeProperties = null
            );

        public Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<TaskEntity>> ListAsync(
            Func<IQueryable<TaskEntity>, IOrderedQueryable<TaskEntity>>? sortedBy
            , Expression<Func<TaskEntity, bool>>[]? filters = null
            , CancellationToken cancellationToken = default
            , Expression<Func<TaskEntity, object>>[]? includeProperties = null
        );

        public Task AddAsync(TaskEntity task, CancellationToken cancellationToken = default);

        public Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);

        public Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default);

    }
}
