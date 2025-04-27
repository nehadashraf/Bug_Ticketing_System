using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
        Task<IEnumerable<User>> GetUsersAssignedToBugAsync(Guid bugId);
    }
}
