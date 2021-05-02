using System;
using System.Collections.Generic;
using System.Text;

namespace DocFlow.Web.Shared.Transactions
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid IncomingStatusId { get; set; }
        public string IncomingStatusName { get; set; }
        public Guid OutgoingStatusId { get; set; }
        public string OutgoingStatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
