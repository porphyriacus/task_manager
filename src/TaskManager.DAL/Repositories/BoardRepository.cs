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
    internal class BoardRepository : IBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Board> _boards;

        public BoardRepository(AppDbContext context, DbSet<Board> boards)
        {
            _context = context;
            _boards = boards;
        }

        public async Task<Board> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<Board, object>>[]? includeProperties)
        {
            var query = _boards.AsQueryable();
            if(includeProperties != null && includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    if(property != null)
                        query = query.Include(property);
                }

            }

            var board = await query.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if(board != null)
                return board;

            throw new KeyNotFoundException($"Task with ID {id} not found");

        }
        public async Task<IReadOnlyCollection<Board>> ListAsync(
            List<Expression<Func<Board, bool>>>? filters = null
            , CancellationToken cancellationToken = default
            , params Expression<Func<Board, object>>[]? includeProperties)
        {
            var query = _boards.AsQueryable();
            if(filters != null && filters.Any())
            {
                foreach(var filter in filters)
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

        public async Task AddAsync(Board board, CancellationToken cancellationToken = default)
        {
           
            await _boards.AddAsync(board, cancellationToken);
        }

        public async Task UpdateAsync(Board board, CancellationToken cancellationToken = default)
        {
            var exist = await _boards.FirstOrDefaultAsync(b => b.Id == board.Id, cancellationToken);
            if (exist == null)
            {
                throw new InvalidOperationException($"Board with ID {board.Id} not found");
            }
            _context.Entry(exist).CurrentValues.SetValues(board);
        }

        public async Task DeleteAsync(Board board, CancellationToken cancellationToken = default)
        {
            var exist = await _boards.FirstOrDefaultAsync(b => b.Id == board.Id, cancellationToken);
            if (exist == null)
            {
                throw new InvalidOperationException($"Board with ID {board.Id} not found");
            }
            _boards.Remove(exist);
        }
    }
}
