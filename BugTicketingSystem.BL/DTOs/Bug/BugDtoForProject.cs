using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.DTOs.Bug
{
    public class BugDtoForProject
    {
        public Guid BugId { get; set; }
        public required string Title { get; set; }
    }
}
