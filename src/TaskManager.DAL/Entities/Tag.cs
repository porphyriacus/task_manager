using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class Tag
    {
        private readonly List<Task> _tasks = new();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();
        public void ChangeName(string newName)
        {
            if(!System.String.IsNullOrEmpty(newName))
                Name = newName;
        }
    }
}
