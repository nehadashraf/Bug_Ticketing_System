using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace BugTicketingSystem.DAL.Models
{
    public class Bug
    {
        public Guid BugId { get; set; }
        public string? Title { get; set; }
        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
        public ICollection<Attachment> BugAttach { get; set; } = new List<Attachment>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
