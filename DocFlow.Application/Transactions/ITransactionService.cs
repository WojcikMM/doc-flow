using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocFlow.Application.Transactions.Commands;
using DocFlow.Application.Transactions.DTOs;

namespace DocFlow.Application.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query);
        Task<IEnumerable<TransactionDto>> GetInitialTransactionsAsync();
        Task<TransactionDto> GetTransactionByIdAsync(Guid id);
        Task CreateTransactionAsync(CreateTransactionCommand command);
        Task UpdateTransactionAsync(Guid Id, UpdateTransactionCommand command);
    }
}
