using DocFlow.Core.Abstractions;
using System;
using System.Collections.Generic;
using DocFlow.Core.Exceptions;

namespace DocFlow.Core.Domain
{
    public class StatusEntity : Entity
    {
        public StatusEntity(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public string Name { get; protected set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new AggregateValidationException("Status name cannot be empty or whitespaced.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual IEnumerable<TransactionEntity> AvailableTransactions { get; protected set; }
        public virtual IEnumerable<TransactionEntity> IncomingTransactions { get; protected set; }
        public virtual IEnumerable<ApplicationEntity> ApplicationsWithStatus { get; protected set; }

    }
}
