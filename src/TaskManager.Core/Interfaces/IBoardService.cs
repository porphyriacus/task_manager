using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.BoardDTO;
using TaskManager.Core.DTOs.TaskDTO;

namespace TaskManager.Core.Interfaces
{
    public interface IBoardService
    {
        public Task<List<BoarsResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<BoarsResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        public Task<BoarsResponseDto> CreateAsync(BoardCreateDto create, CancellationToken cancellationToken);
        public Task<BoarsResponseDto> UpdateAsync(BoardUpdateDto create, CancellationToken cancellationToken);
        public Task<BoarsResponseDto> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
