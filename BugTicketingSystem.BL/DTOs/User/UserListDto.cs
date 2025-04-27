namespace BugTicketingSystem.BL.DTOs
{
    public class UserListDto
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; }
    }
}