using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.BL.DTOs;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTicketingSystem.BL.Mangers.Bugs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugsController : ControllerBase
    {
        private readonly IBugManager _bugManager;

        public BugsController(IBugManager bugManager)
        {
            _bugManager = bugManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BugDto>> CreateNewBug(BugCreationDto bugCreationDto)
        {
            try
            {
                var result = await _bugManager.CreateBugAsync(bugCreationDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugDto>>> GetBugs()
        {
            var bugs = await _bugManager.GetAllBugsAsync();
            return Ok(bugs);
        }

       [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<BugDto>> GetBugDetails(Guid Id)
        {
            try
            {
                var bug = await _bugManager.GetBugByIdAsync(Id);
                return Ok(bug);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

       // [Authorize]
        [HttpPost("{Id:guid}/assignees")]
        public async Task<ActionResult> AssignBugToUser(Guid Id, [FromBody] Guid UserId)
        {
            try
            {
                var result = await _bugManager.AssignUserToBugAsync(Id, UserId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{Id:guid}/assignees/{UserId:guid}")]
        public async Task<ActionResult> DeleteBugFromUser(Guid Id, Guid UserId)
        {
            try
            {
                await _bugManager.RemoveUserFromBugAsync(Id, UserId);
                return Ok("Successfully: Removing User from Bug");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{Id:guid}/attachments")]
        public async Task<ActionResult> UploadAttachment(Guid Id, IFormFile file)
        {
            try
            {
                var result = await _bugManager.AddAttachmentAsync(Id, file);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{Id}/attachments")]
        public async Task<ActionResult> GetAttachments(Guid Id)
        {
            var attachments = await _bugManager.GetAttachmentsAsync(Id);
            return Ok(attachments);
        }

        [Authorize]
        [HttpDelete("{Id}/attachments/{attachmentId}")]
        public async Task<ActionResult> DeleteAttachment(Guid Id, Guid attachmentId)
        {
            try
            {
                var result = await _bugManager.DeleteAttachmentAsync(Id, attachmentId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
