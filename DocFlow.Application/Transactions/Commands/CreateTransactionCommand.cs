using System;

namespace DocFlow.Application.Transactions.Commands
{
    public class CreateTransactionCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IncomingStatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }
    }
}
