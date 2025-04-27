using BugTicketingSystem.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Mangers.Projects
{
    public interface IProjectManager
    {
        Task<ProjectDto> CreateProjectAsync(ProjectCreationDto projectCreationDto);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(Guid projectId);
    }
}
