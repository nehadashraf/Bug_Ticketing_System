using BugTicketingSystem.BL.DTOs.Bug;

namespace BugTicketingSystem.BL.DTOs
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<BugDtoForProject> ProjectBugs { get; set; } = new List<BugDtoForProject>();

    }
}
