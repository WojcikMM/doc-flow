using DocFlow.Core.Abstractions;
using DocFlow.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocFlow.Core.Repositories
{
    public interface ITransactionRepository : IRepository<TransactionEntity>
    {
        Task<IEnumerable<TransactionEntity>> GetByIncomingStatusAsync(Guid? statusId);
        Task<IEnumerable<TransactionEntity>> GetStartingTransactions();
    }
}
