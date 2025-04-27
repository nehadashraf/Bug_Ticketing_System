using BugTicketingSystem.BL.DTOs;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.AttachmentRepository;
using BugTicketingSystem.DAL.Repositories.ProjectRepository;
using BugTicketingSystem.DAL.Repositories.UserRepository;
using Microsoft.AspNetCore.Http;

namespace BugTicketingSystem.BL.Mangers.Bugs
{

    public class BugManager : IBugManager
    {
        private readonly IBugRepository _bugRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public BugManager(
            IBugRepository bugRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IAttachmentRepository attachmentRepository)
        {
            _bugRepository = bugRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<BugDto> CreateBugAsync(BugCreationDto bugCreationDto)
        {
            var project = await _projectRepository.GetByIdAsync(bugCreationDto.ProjectId);
            if (project == null)
            {
                throw new InvalidOperationException("Project does not exist");
            }

            var bug = new Bug
            {
                Title = bugCreationDto.Title,
                ProjectId = bugCreationDto.ProjectId
            };

            await _bugRepository.AddAsync(bug);
            await _bugRepository.SaveChangesAsync();

            var createdBug = await _bugRepository.GetBugWithDetailsAsync(bug.BugId);
            var assignees = await _userRepository.GetUsersAssignedToBugAsync(bug.BugId);

            return new BugDto
            {
                BugId = createdBug.BugId,
                Title = createdBug.Title,
                Project = new ProjectDto
                {
                    ProjectId = createdBug.Project.ProjectId,
                    Name = createdBug.Project.Name,
                    Description = createdBug.Project.Description,
                },
                Assignees = assignees.Select(user => new UserBugDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles.Select(role => role.ToString()).ToList()
                }).ToList()
            };
        }

        public async Task<IEnumerable<BugDto>> GetAllBugsAsync()
        {
            var bugs = await _bugRepository.GetAllBugsWithDetailsAsync();

            return bugs.Select(b => new BugDto
            {
                BugId = b.BugId,
                Title = b.Title,
                Project = new ProjectDto
                {
                    ProjectId = b.Project.ProjectId,
                    Name = b.Project.Name,
                    Description = b.Project.Description
                },
                Assignees = b.Users.Select(user => new UserBugDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles.Select(role => role.ToString()).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task<BugDto> GetBugByIdAsync(Guid bugId)
        {
            var bug = await _bugRepository.GetBugWithDetailsAsync(bugId);
            if (bug == null)
            {
                throw new KeyNotFoundException("Bug not found");
            }

            var assignees = await _userRepository.GetUsersAssignedToBugAsync(bugId);

            return new BugDto
            {
                BugId = bug.BugId,
                Title = bug.Title,
                Project = new ProjectDto
                {
                    ProjectId = bug.Project.ProjectId,
                    Name = bug.Project.Name,
                    Description = bug.Project.Description
                },
                Assignees = assignees.Select(user => new UserBugDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles.Select(role => role.ToString()).ToList()
                }).ToList()
            };
        }

        public async Task<UserBugDto> AssignUserToBugAsync(Guid bugId, Guid userId)
        {
            await _bugRepository.AssignUserToBugAsync(bugId, userId);
            await _bugRepository.SaveChangesAsync();

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return new UserBugDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(role => role.ToString()).ToList()
            };
        }

        public async Task RemoveUserFromBugAsync(Guid bugId, Guid userId)
        {
            await _bugRepository.RemoveUserFromBugAsync(bugId, userId);
            await _bugRepository.SaveChangesAsync();
        }

        public async Task<string> AddAttachmentAsync(Guid bugId, IFormFile file)
        {
            var bug = await _bugRepository.GetByIdAsync(bugId);
            if (bug == null)
            {
                throw new KeyNotFoundException("Bug not found");
            }

            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("No file uploaded");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                AttachmentId = Guid.NewGuid(),
                FileName = file.FileName,
                FilePath = filePath,
                FileType = file.ContentType,
                FileSize = file.Length,
                BugId = bugId
            };

            await _attachmentRepository.AddAsync(attachment);
            await _attachmentRepository.SaveChangesAsync();

            return "File uploaded successfully";
        }

        public async Task<IEnumerable<object>> GetAttachmentsAsync(Guid bugId)
        {
            var attachments = await _attachmentRepository.GetAttachmentsByBugIdAsync(bugId);

            return attachments.Select(a => new
            {
                a.AttachmentId,
                a.FileName,
                a.FileType,
                a.FileSize
            }).ToList();
        }

        public async Task<string> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
        {
            var attachments = await _attachmentRepository.GetAttachmentsByBugIdAsync(bugId);
            var attachment = attachments.FirstOrDefault(a => a.AttachmentId == attachmentId);

            if (attachment == null)
            {
                throw new KeyNotFoundException("Attachment not found");
            }

            if (System.IO.File.Exists(attachment.FilePath))
            {
                System.IO.File.Delete(attachment.FilePath);
            }

            await _attachmentRepository.DeleteAsync(attachment);
            await _attachmentRepository.SaveChangesAsync();

            return "Attachment deleted successfully";
        }
    }
}
