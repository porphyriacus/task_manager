using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; private set; }
        public TaskPriority Priority { get; private set; }
    }
}
