using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class Board
    {
        private readonly List<Task> _tasks = new();
        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; set; }

        public string OwnerId { get; private set; }
        public User Owner { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public IReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();

        protected Board() { }
        public Board(int projectId, string ownerId, string name, string description)
        {

            if (System.String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (!System.String.IsNullOrEmpty(description))
                Description = description;

            ProjectId = projectId;
            OwnerId = ownerId;
            Name = name;
        }
    }
}
