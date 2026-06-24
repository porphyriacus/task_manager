using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DAL.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public int TaskId { get; private set; }
        public TaskEntity Task { get; private set; }

        public string Text { get; private set; }

        public string OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        protected Comment() { }
        public Comment(  string text, string ownerId)
        {

            if (System.String.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            Text = text;
            OwnerId = ownerId;
            CreatedAt = DateTime.Now;
        }
    }
}
