using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoDashboard.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string OriginalFileName { get; set; } = string.Empty;
        [Required]
        public string StoredFilePath { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        public string FileType { get; set; } = string.Empty;
        public int FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public int UploadedBy { get; set; }
        public int? ProjectId { get; set; }
    }
}
