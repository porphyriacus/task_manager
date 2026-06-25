using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Task cant exist without board")]  // required is not requerd here(lol) just for message 
        [Range(1, int.MaxValue, ErrorMessage = "BoardId must be greater than 0")]
        public int BoardId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Can not be longer than 100 symbols and shorter than 3 symbols")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Can not be longer than 1000 symbols")]
        public string? Description { get;  set; }
        public DateTime? Deadline { get; set; }
        public TaskPriority? Priority { get; set; }
    }
}
