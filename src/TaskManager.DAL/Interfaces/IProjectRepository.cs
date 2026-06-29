using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Interfaces
{
    public interface IProjectRepository
    {
        public Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<Project, object>>[]? includeProperties);
        public Task<IReadOnlyCollection<Project>> ListAsync(
            List<Expression<Func<Project, bool>>>? filters = null
            , CancellationToken cancellationToken = default
            , params Expression<Func<Project, object>>[]? includeProperties);

        public Task AddAsync(Project project, CancellationToken cancellationToken = default);

        public Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

        public Task DeleteAsync(Project project, CancellationToken cancellationToken = default);
    }
}
