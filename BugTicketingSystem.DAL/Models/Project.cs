namespace BugTicketingSystem.DAL.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<Bug> ProjectBugs { get; set; } = new List<Bug>();

    }
}
