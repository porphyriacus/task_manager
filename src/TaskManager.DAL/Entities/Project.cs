
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager.DAL.Entities
{
    public class Project
    {
        private readonly List<Board> _boards = new();
        private readonly List<User> _users = new();

        public int Id { get; private set; }
        public string OwnerId { get; private set; }
        public User Owner { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public IReadOnlyCollection<Board> Boards { get =>  _boards.AsReadOnly(); }
        public IReadOnlyCollection<User> Users { get => _users.AsReadOnly(); }

        protected Project() { }
        public Project(string  ownerId, string name, string description)
        {
            if (System.String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (!System.String.IsNullOrEmpty(description))
                Description = description;

            OwnerId = ownerId;
            Name = name;
            CreatedAt = DateTime.UtcNow;
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
