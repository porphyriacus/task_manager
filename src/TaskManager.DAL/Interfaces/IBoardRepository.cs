using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Interfaces
{
    public interface IBoardRepository
    {
        public Task<Board> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<Board, object>>[]? includeProperties);
        public Task<IReadOnlyCollection<Board>> ListAsync(
            List<Expression<Func<Board, bool>>>? filters = null
            , CancellationToken cancellationToken = default
            , params Expression<Func<Board, object>>[]? includeProperties);

        public Task AddAsync(Board board, CancellationToken cancellationToken = default);

        public Task UpdateAsync(Board board, CancellationToken cancellationToken = default);

        public Task DeleteAsync(Board board, CancellationToken cancellationToken = default);
    }
}
