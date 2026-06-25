using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is requered")]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Can not be longer than 1000 symbols")]
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
