using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;

namespace BugTicketingSystem.BL.Mangers.Bugs
{
   public interface IBugRepository : IGenericRepository<Bug>
    {
        Task<Bug> GetBugWithDetailsAsync(Guid bugId);
        Task<IEnumerable<Bug>> GetAllBugsWithDetailsAsync();
        Task AssignUserToBugAsync(Guid bugId, Guid userId);
        Task RemoveUserFromBugAsync(Guid bugId, Guid userId);
    }
}

