using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.DTOs.ProjectDTO
{
    /*
             public int Id { get; private set; }
        public string OwnerId { get; private set; }
        public User Owner { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
     */
    public class ProjectCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Can not be longer than 500 symbols and shorter than 3 symbols")]
        public string? Description { get; set; }
    }
}
