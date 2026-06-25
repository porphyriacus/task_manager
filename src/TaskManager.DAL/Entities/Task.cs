using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Enums;

namespace TaskManager.DAL.Entities
{
    public class TaskEntity
    {
        private readonly List<User> _executers = new();
        private readonly List<Tag> _tags = new();
        private readonly List<Comment> _comments = new();

        public int Id { get; private set; }
        public int BoardId { get; private set; }
        public Board Board { get; private set; }
        public string OwnerId { get; private set; }
        public User Owner { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTime? Deadline { get; private set; }
        public TaskPriority Priority { get; private set; }

        public IReadOnlyCollection<User> Executers => _executers.AsReadOnly();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        protected TaskEntity() { }

        public TaskEntity(int boardId, string ownerId, string name, string description, DateTime? deadline, TaskPriority priority)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            BoardId = boardId;
            OwnerId = ownerId;
            Name = name;
            Description = description ?? string.Empty;
            Deadline = deadline;
            Priority = priority;
        }

        public void ChangeName(string newName)
        {
            if(System.String.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            Name = newName;
        }
        public void ChangeDeadline(DateTime? deadline)
        {
            Deadline = deadline ?? null;
        }
        public void ChangePriority(TaskPriority priority)
        {
            Priority = priority;
        }
        public void ChangeDescription(string newDescription)
        {
            Description = newDescription ?? string.Empty;
        }


    }
}
