using System.ComponentModel.DataAnnotations.Schema;

namespace BugTicketingSystem.DAL.Models
{
    public class Attachment
    {
        public Guid AttachmentId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileType { get; set; }
        public long FileSize { get; set; }
        [ForeignKey("Bug")]
        public Guid? BugId { get; set; }
        public Bug? Bug { get; set; }
    }
}
