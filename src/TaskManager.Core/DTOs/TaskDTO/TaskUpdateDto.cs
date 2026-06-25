using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskPriority? Priority { get; set; }
    }
}
