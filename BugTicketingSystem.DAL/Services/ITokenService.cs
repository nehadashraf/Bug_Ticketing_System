using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.DAL.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
