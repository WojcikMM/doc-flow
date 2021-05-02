using System;

namespace DocFlow.Application.Transactions.Commands
{
    public class UpdateTransactionCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IncomingStatusId { get; set; }
        public Guid? OutgoingStatusId { get; set; }
        public bool? IsStartingTransaction { get; set; }
    }
}
