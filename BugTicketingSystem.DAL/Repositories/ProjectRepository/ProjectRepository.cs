using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.ProjectRepository
{
    public class ProjectRepository(ApplicationDbContext dbContext) : GenericRepository<Project>(dbContext), IProjectRepository
    {
        // In your repository


        public async Task<Project> GetProjectWithDetailsAsync(Guid projectId)
        {
            return await _dbContext.Projects
                .Include(p => p.ProjectBugs) 
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<Project> FindByNameAsync(string projectName)
        {
            return await _dbContext.Projects
                .FirstOrDefaultAsync(p => p.Name.ToLower() == projectName.ToLower());
        }
    }

}
