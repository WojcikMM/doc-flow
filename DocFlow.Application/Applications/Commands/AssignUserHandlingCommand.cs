using System;

namespace DocFlow.Application.Applications.Commands
{
    public class AssignUserHandlingCommand
    {
        public Guid ApplicationId { get; set; }
        public string UserId { get; set; }
    }
}
