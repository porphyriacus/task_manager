using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskFilteredDto
    {
        public string SearchTerm { get; set; }
        public int? BoardId { get; set; } // if (BoardId == null) -> all tasks
        public string SortedBy { get; set; } // "deadline", "priority", "name"
        public bool IsDescending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
