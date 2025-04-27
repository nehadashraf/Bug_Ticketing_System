using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepository
{
    public class AttachmentRepository(ApplicationDbContext dbContext) : GenericRepository<Attachment>(dbContext), IAttachmentRepository
    {
        public async Task<IEnumerable<Attachment>> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            return await _dbContext.Attachments
                .Where(a => a.BugId == bugId)
                .ToListAsync();
        }
    }
}
