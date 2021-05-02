using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DocFlow.Core.Domain
{
    public class UserEntity : IdentityUser
    {
        public UserEntity() : base()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public UserEntity(string Id, string Name) : base(Name)
        {
            this.Id = Id;
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public virtual IEnumerable<TransactionItem> TransactionItems { get; protected set; }
        public virtual IEnumerable<ApplicationEntity> AssignedApplications { get; protected set; }
    }
}