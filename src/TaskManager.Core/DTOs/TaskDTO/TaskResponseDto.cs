using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Enums;
using TaskManager.DAL.Entities;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskSummaryDto
    {
        public int Id { get; set; }

        public int BoardId { get; set; }
        public string Boardname { get; set; }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskPriority Priority { get; set; }

    }
}
