using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.BL.DTOs
{
    public class BugDto
    {
        public Guid BugId { get; set; }
        public required string Title { get; set; }
        public ProjectDto? Project { get; set; }
        public ICollection<UserBugDto> Assignees { get; set; } = new List<UserBugDto>();
       // public ICollection<Attachment> BugAttach { get; set; } = new List<Attachment>();


    }
}
