using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core.DTOs.TaskDTO
{
    public class TaskFilteredDto
    {
        public string? SearchTerm { get; set; }         // if null -> all tasks
        public int? BoardId { get; set; }               // if (BoardId == null) -> all tasks
        public string? SortedBy { get; set; }           // "deadline", "priority", "name" 
                                                        // if null -> sorted by id
        public bool IsDescending { get; set; } = false;

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
    }
}
