using DocFlow.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using DocFlow.Core.Exceptions;

namespace DocFlow.Core.Domain
{

    public class ApplicationEntity : Entity
    {
        public Guid? StatusId
        {
            get
            {
                return TransactionItems?.ToList()?.LastOrDefault()?.OutgoingStatusId;
            }
        }
        public string AssignedUserId { get; protected set; }

        public virtual UserEntity User { get; protected set; }
        public virtual IList<TransactionItem> TransactionItems { get; protected set; }


        public ApplicationEntity(Guid Id)
        {
            this.Id = Id;
            TransactionItems = new List<TransactionItem>();
        }
        public ApplicationEntity(Guid Id, string AssignedUserId, IEnumerable<TransactionItem> transactionItems)
        {
            this.Id = Id;
            this.AssignedUserId = AssignedUserId;
            TransactionItems = transactionItems.ToList();
        }


        public void ApplyTransaction(TransactionEntity transaction, string userId)
        {
            if (StatusId != null && transaction.IncomingStatusId != StatusId)
            {
                throw new AggregateIllegalLogicException("Wrong transaction. Check configuration.");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new AggregateValidationException("Invalid user id.");
            }

            TransactionItems.Add(TransactionItem.Create(transaction, userId.ToString(), Id));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssingnToHandling(string userId)
        {
            if ( string.IsNullOrWhiteSpace(userId))
            {
                throw new AggregateValidationException("Invalid user id");
            }
            if (string.IsNullOrWhiteSpace(AssignedUserId) && AssignedUserId != userId)
            {
                throw new AggregateIllegalLogicException("Application assigned to another user. Need to release one first.");
            }

            AssignedUserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReleaseHandling()
        {
            AssignedUserId = null;
            UpdatedAt = DateTime.UtcNow;
        }

    }

}
