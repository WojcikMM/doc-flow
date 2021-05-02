using DocFlow.Core.Abstractions;
using System;
using System.Collections.Generic;
using DocFlow.Core.Exceptions;

namespace DocFlow.Core.Domain
{
    public class TransactionEntity : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Guid? IncomingStatusId { get; protected set; }
        public Guid OutgoingStatusId { get; protected set; }

        public virtual StatusEntity IncomingStatus { get; protected set; }
        public virtual StatusEntity OutgoingStatus { get; protected set; }
        public virtual IEnumerable<TransactionItem> TransactionItems { get; protected set; }

        public TransactionEntity(Guid id, string name, string description, Guid? incomingStatusId, Guid outgoingStatusId)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            SetName(name);
            SetDescription(description);
            SetIncomingStatus(incomingStatusId);
            SetOutgoingStatus(outgoingStatusId);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new AggregateValidationException("Name cannot be empty or only whitespaced.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetIncomingStatus(Guid? statusId)
        {
            IncomingStatusId = statusId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetOutgoingStatus(Guid statusId)
        {
            if (statusId == Guid.Empty)
            {
                throw new AggregateValidationException("Cannot assign empty status as incoming.");
            }
            OutgoingStatusId = statusId;
            UpdatedAt = DateTime.UtcNow;
        }



    }

}
