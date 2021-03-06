using System;

using Ideas.Domain.Core;

namespace Ideas.Domain.AggregatesModel
{
    public class Idea
        : Entity, IAggregateRoot
    {
        public DateTime CreatedAt { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        protected Idea()
        {
        }

        public Idea(string title, string description = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Title = title;
            Description = description;
        }
    }
}