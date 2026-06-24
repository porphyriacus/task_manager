using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace TaskManager.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public ICollection<Project> AssignedProjects { get; set; } = new List<Project>();

        public ICollection<TaskEntity> CreatedTasks { get; set; } = new List<TaskEntity>();
        public ICollection<TaskEntity> ExecutedTasks { get; set; } = new List<TaskEntity>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
