using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.DTOs.BoardDTO
{
    public class BoardCreateDto
    {
        [Required(ErrorMessage = "Board cant exist without project")]
        [Range(1, int.MaxValue, ErrorMessage = "ProjectId must be greater than 0")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Can not be longer than 500 symbols and shorter than 3 symbols")] 
        public string? Description { get; set; }
    }
}
