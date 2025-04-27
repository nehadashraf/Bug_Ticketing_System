namespace BugTicketingSystem.BL.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
