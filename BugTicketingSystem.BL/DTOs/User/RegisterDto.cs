
namespace BugTicketingSystem.BL.DTOs
{
    public class RegisterDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
