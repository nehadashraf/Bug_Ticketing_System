using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.ProjectRepository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project> GetProjectWithDetailsAsync(Guid projectId);
        Task<Project> FindByNameAsync(string projectName);

    }
}
