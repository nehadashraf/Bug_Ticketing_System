using BugTicketingSystem.BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BugTicketingSystem.BL.Mangers.Projects;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManager _projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreationDto projectCreationDto)
        {
            var result = await _projectManager.CreateProjectAsync(projectCreationDto);

            if (result == null)
            {
                return BadRequest("A project with the same name already exists.");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectManager.GetAllProjectsAsync();
            return Ok(projects);
        }

        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<ProjectDto>> GetProjectDetails(Guid Id)
        {
            try
            {
                var project = await _projectManager.GetProjectByIdAsync(Id);
                return Ok(project);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }

}
