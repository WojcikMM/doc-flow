using DocFlow.Core.Abstractions;
using System;

namespace DocFlow.Core.Domain
{
    public class TransactionItem : IValueObject
    {
        public Guid Id { get; protected set; }
        public Guid ApplicationId { get; protected set; }
        public string UserId { get; protected set; }
        public Guid TransactionId { get; protected set; }
        public string TransactionName { get; protected set; }
        public string TransactionDescription { get; protected set; }
        public Guid OutgoingStatusId { get; protected set; }
        public string OutgoingStatusName { get; protected set; }
        public DateTime TransactionAt { get; protected set; }

        public virtual TransactionEntity Transaction { get; protected set; }
        public virtual UserEntity User { get; protected set; }
        public virtual ApplicationEntity Application { get; protected set; }

        protected TransactionItem()
        {
            Id = Guid.NewGuid();
        }
        protected TransactionItem(TransactionEntity transaction, string userId, Guid applicationId)
        {
            Id = Guid.NewGuid();
            ApplicationId = applicationId;
            TransactionId = transaction.Id;
            TransactionName = transaction.Name;
            TransactionDescription = transaction.Description;
            OutgoingStatusId = transaction.OutgoingStatusId;
            TransactionAt = DateTime.UtcNow;
            UserId = userId;
        }

        public static TransactionItem Create(TransactionEntity transaction, string userId, Guid applicationId) =>
            new TransactionItem(transaction, userId, applicationId);

    }

}
