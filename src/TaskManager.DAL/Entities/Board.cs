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

        public void ChangeName(string newName)
        {
            if (System.String.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            Name = newName;
        }
        public void ChangeDescription(string newDescription)
        {
            if (System.String.IsNullOrEmpty(newDescription))
                throw new ArgumentNullException(nameof(newDescription));
            Description = newDescription;
        }
    }
}
