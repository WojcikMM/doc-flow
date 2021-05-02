using System;

namespace DocFlow.Web.Shared.Applications
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
