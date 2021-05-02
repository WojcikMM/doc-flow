using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocFlow.Application.Transactions.DTOs;
using DocFlow.Core.Domain;
using DocFlow.Core.Repositories;
using DocFlow.Application.Transactions;
using DocFlow.Core.Exceptions;
using DocFlow.Application.Transactions.Commands;

namespace DocFlow.Infrastructure.Services
{
    public class TrasactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;

        public TrasactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query)
        {
            var transactions = await transactionRepository.GetAllAsync();
            return mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task CreateTransactionAsync(CreateTransactionCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid transaction id");
            }

            var transaction = await transactionRepository.GetAsync(command.Id);

            if (transaction != null)
            {
                throw new AggregateIllegalLogicException("Cannot create transaciton with given id. Transaction exists.");
            }

            var application = new TransactionEntity(
                command.Id,
                command.Name,
                command.Description,
                command.IncomingStatusId,
                command.OutgoingStatusId);

            await transactionRepository.CreateAsync(application);
        }

        public async Task<IEnumerable<TransactionDto>> GetInitialTransactionsAsync()
        {
            var result = await transactionRepository.GetByIncomingStatusAsync(null);
            return mapper.Map<IEnumerable<TransactionDto>>(result);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await GetByIdAsync(id);
            return mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateTransactionAsync(Guid Id, UpdateTransactionCommand command)
        {
            var transaction = await GetByIdAsync(Id);

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                transaction.SetName(command.Name);
            }

            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                transaction.SetDescription(command.Description);
            }

            if (command.IncomingStatusId.HasValue && command.IsStartingTransaction.HasValue && command.IsStartingTransaction.Value == true)
            {
                throw new AggregateIllegalLogicException("Cannot update status. You specify incoming status and \"IsStartingTransaction\" value. There is no logic.");
            }

            if (command.IncomingStatusId.HasValue)
            {
                transaction.SetIncomingStatus(command.IncomingStatusId.Value);
            }

            if (command.IsStartingTransaction == true)
            {
                transaction.SetIncomingStatus(null);
            }

            if (command.OutgoingStatusId.HasValue)
            {
                transaction.SetOutgoingStatus(command.OutgoingStatusId.Value);
            }

            await transactionRepository.UpdateAsync(transaction);
        }

        private async Task<TransactionEntity> GetByIdAsync(Guid Id)
        {
            return await transactionRepository.GetAsync(Id);
        }
    }
}
