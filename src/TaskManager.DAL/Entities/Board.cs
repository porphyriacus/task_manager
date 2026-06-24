using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DAL.Entities
{
    public class Board
    {
        private readonly List<TaskEntity> _tasks = new();
        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public IReadOnlyCollection<TaskEntity> Tasks => _tasks.AsReadOnly();

        protected Board() { }
        public Board(int projectId,  string name, string description)
        {

            if (System.String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
           
            ProjectId = projectId;
            Name = name;
            Description = description ?? string.Empty;
        }
    }
}
