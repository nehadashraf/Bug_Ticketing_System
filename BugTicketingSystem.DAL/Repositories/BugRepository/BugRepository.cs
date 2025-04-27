using BugTicketingSystem.BL.Mangers.Bugs;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

public class BugRepository(ApplicationDbContext dbContext) : GenericRepository<Bug>(dbContext), IBugRepository
{
    public async Task<Bug> GetBugWithDetailsAsync(Guid bugId)
    {
        return await _dbContext.Bugs
            .Include(b => b.Project)
            .Include(b => b.Users)
            .FirstOrDefaultAsync(b => b.BugId == bugId);
    }
    public async Task<IEnumerable<Bug>> GetAllBugsWithDetailsAsync()
    {
        return await _dbContext.Bugs
            .Include(b => b.Project)
            .Include(b => b.Users)
        .ToListAsync();
    }

    public async Task AssignUserToBugAsync(Guid bugId, Guid userId)
    {
        var bug = await GetBugWithDetailsAsync(bugId);
        var user = await _dbContext.Users.FindAsync(userId);

        if (bug != null && user != null && !bug.Users.Any(u => u.Id == userId))
        {
            bug.Users.Add(user);
        }
    }

    public async Task RemoveUserFromBugAsync(Guid bugId, Guid userId)
    {
        var bug = await GetBugWithDetailsAsync(bugId);
        var user = await _dbContext.Users.FindAsync(userId);

        if (bug != null && user != null && bug.Users.Any(u => u.Id == userId))
        {
            bug.Users.Remove(user);
        }
    }
}
