using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.DTOs.BoardDTO
{
    public class BoarsResponseDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
