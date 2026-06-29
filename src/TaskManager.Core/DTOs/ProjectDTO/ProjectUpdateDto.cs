using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.DTOs.ProjectDTO
{
    public class ProjectUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Can not be longer than 500 symbols and shorter than 3 symbols")]
        public string? Description { get; set; } = null;
    }
}
