using System;

namespace DocFlow.Application.Applications.Commands
{
    public class CreateApplicationCommand
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public Guid InitialTransactionId { get; set; }
        public string RegistrationUser { get; set; }
    }
}
