using BugTicketingSystem.BL.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Mangers.Bugs
{
    public interface IBugManager
    {
        Task<BugDto> CreateBugAsync(BugCreationDto bugCreationDto);
        Task<IEnumerable<BugDto>> GetAllBugsAsync();
        Task<BugDto> GetBugByIdAsync(Guid bugId);
        Task<UserBugDto> AssignUserToBugAsync(Guid bugId, Guid userId);
        Task RemoveUserFromBugAsync(Guid bugId, Guid userId);
        Task<string> AddAttachmentAsync(Guid bugId, IFormFile file);
        Task<IEnumerable<object>> GetAttachmentsAsync(Guid bugId);
        Task<string> DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
    }
}
