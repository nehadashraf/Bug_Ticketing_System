using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.UserRepository
{
    public class UserRepository (ApplicationDbContext dbContext) :GenericRepository<User>(dbContext), IUserRepository
    {
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAssignedToBugAsync(Guid bugId)
        {
            return await _dbContext.Users
                .Where(u => u.Bugs.Any(b => b.BugId == bugId))
                .ToListAsync();
        }
    }
}
