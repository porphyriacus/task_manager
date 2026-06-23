using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public int TaskId { get; private set; }
        public Task Task { get; private set; }

        public string Name { get; private set; }
        public string Text { get; private set; }

        public string OwnerId { get; private set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        protected Comment() { }
        public Comment(int id, string name, string text, string ownerId)
        {
            if(System.String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (System.String.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            Id = id;
            Name = name;
            Text = text;
            OwnerId = ownerId;
            CreatedDate = DateTime.Now;
        }
    }
}
