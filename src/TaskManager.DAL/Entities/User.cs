using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Core.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
        public ICollection<Task> ExecutedTasks { get; set; } = new List<Task>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
