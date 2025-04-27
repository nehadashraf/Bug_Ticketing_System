
namespace BugTicketingSystem.BL.DTOs;

public class BugCreationDto
{
    public required string Title { get; set; }
    public Guid ProjectId { get; set; }
}
