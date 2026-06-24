using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DAL.Entities
{
    public class Tag
    {
        private readonly List<TaskEntity> _tasks = new();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<TaskEntity> Tasks => _tasks.AsReadOnly();
        public void ChangeName(string newName)
        {
            if(!System.String.IsNullOrEmpty(newName))
                Name = newName;
        }

        protected Tag() { }

        public Tag(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
