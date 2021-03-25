using System;

namespace Ideas.API.Models
{
    public class IdeaSummaryViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}