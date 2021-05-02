using System;

namespace DocFlow.Application.Applications.Commands
{
    public class ApplyTransactionCommand
    {
        public Guid ApplicationId { get; set; }
        public string UserId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
