using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.BoardDTO;
using TaskManager.Core.DTOs.ProjectDTO;
using TaskManager.Core.Interfaces;
using TaskManager.DAL.Data;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Interfaces;

namespace TaskManager.Core.Services
{
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _context;
        private readonly IBoardRepository _boardRepository;
        public BoardService(AppDbContext appDbContext
                              , IBoardRepository boardRepository)
        {

            _context = appDbContext;
            _boardRepository = boardRepository;
        }
        public async Task<List<BoarsResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var projects = await _boardRepository.ListAsync(null, cancellationToken
                                                        , boards => boards.Project);
            return projects.Select(MapToResponseDto).ToList();

        }
        public async Task<BoarsResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var board = await _boardRepository.GetByIdAsync(id, cancellationToken
                                            , t => t.Project);
                if (board == null)
                {
                    throw new KeyNotFoundException($"board with ID {id} not found");
                }

                return MapToResponseDto(board);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"board with ID {id} not found");
            }
        }

        public async Task<BoarsResponseDto> CreateAsync(BoardCreateDto create, CancellationToken cancellationToken)
        {   
            var exist = await _context.Boards.FirstOrDefaultAsync(b => b.Name == create.Name && b.ProjectId == create.ProjectId, cancellationToken);
            if (exist != null)
            {
                throw new InvalidOperationException("Board with this name already exists");
            }
            var board = new Board(create.ProjectId, create.Name, create.Description);

            await _boardRepository.AddAsync(board, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(board);
        }
        public async Task<BoarsResponseDto> UpdateAsync(BoardUpdateDto dto, CancellationToken cancellationToken)
        {
            Board board = await _boardRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (board == null)
            {
                throw new KeyNotFoundException($"Project with ID {dto.Id} not found");
            }

            board.ChangeName(dto.Name);
            board.ChangeDescription(dto.Description);

            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(board);

        }
        public async Task<BoarsResponseDto> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Board board = await _boardRepository.GetByIdAsync(id, cancellationToken);
            if (board == null)
            {
                throw new KeyNotFoundException($"board with ID {id} not found");
            }

            await _boardRepository.DeleteAsync(board, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(board);
        }


        private static BoarsResponseDto MapToResponseDto(Board board)
        {
            return new BoarsResponseDto
            {
                Id = board.Id,

                ProjectId = board.ProjectId,
                ProjectName = board.Project?.Name ?? "Unknown",

                Name = board.Name,
                Description = board.Description
            };
        }
    }
}
