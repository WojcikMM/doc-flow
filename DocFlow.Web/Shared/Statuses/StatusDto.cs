using System;

namespace DocFlow.Web.Shared.Statuses
{
    public class StatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
