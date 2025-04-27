namespace BugTicketingSystem.BL.DTOs
{
    public class UserBugDto
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

    }
}
