using BugTicketingSystem.BL.DTOs;
using BugTicketingSystem.BL.DTOs.Bug;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.ProjectRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Mangers.Projects
{
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreationDto projectCreationDto)
        {
            var existingProject = await _projectRepository.FindByNameAsync(projectCreationDto.Name);

            if (existingProject != null)
            {
                return null;
            }
            var project = new Project
            {
                Name = projectCreationDto.Name,
                Description = projectCreationDto.Description
            };

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                ProjectBugs = project.ProjectBugs.Select(bug => new BugDtoForProject
                {
                    BugId = bug.BugId,
                    Title = bug.Title,
                }).ToList()
            };
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
            }).ToList();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectWithDetailsAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
            };
        }
    }
}
