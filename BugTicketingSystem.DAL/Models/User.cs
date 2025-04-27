using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.DAL.Models
{
    public class User  :IdentityUser<Guid>
    {
        public List<string> Roles { get; set; } = new List<string>();
        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    }
}
