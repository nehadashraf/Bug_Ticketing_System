using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepository
{
    public interface IAttachmentRepository : IGenericRepository<Attachment>
    {
        Task<IEnumerable<Attachment>> GetAttachmentsByBugIdAsync(Guid bugId);
    }
}
