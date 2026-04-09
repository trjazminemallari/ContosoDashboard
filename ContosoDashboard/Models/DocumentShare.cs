using System;

namespace ContosoDashboard.Models
{
    public class DocumentShare
    {
        public int DocumentId { get; set; }
        public int SharedWithUserId { get; set; }
        public int SharedByUserId { get; set; }
        public DateTime SharedAt { get; set; }
    }
}
