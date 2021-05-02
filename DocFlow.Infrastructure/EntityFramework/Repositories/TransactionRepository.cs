using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocFlow.Core.Domain;
using DocFlow.Core.Repositories;

namespace DocFlow.Infrastructure.EntityFramework.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DocFlowDbContext unitOfWork;

        public TransactionRepository(DocFlowDbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(TransactionEntity entity)
        {
            unitOfWork.Add(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionEntity>> GetAllAsync()
        {
            return await AllEntities().ToListAsync();
        }

        public async Task<TransactionEntity> GetAsync(Guid id)
        {
            return await AllEntities().FirstOrDefaultAsync(transaction => transaction.Id == id);
        }

        public async Task<IEnumerable<TransactionEntity>> GetByIncomingStatusAsync(Guid? statusId)
        {
            return await AllEntities()
                .Where(transaction => transaction.IncomingStatusId == statusId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionEntity>> GetStartingTransactions()
        {
            return await AllEntities()
                .Where(transaction => transaction.IncomingStatusId == null)
                .ToListAsync();
        }

        public async Task UpdateAsync(TransactionEntity entity)
        {
            unitOfWork.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        private IQueryable<TransactionEntity> AllEntities()
        {
            return unitOfWork.Transactions.Include(p => p.IncomingStatus).Include(p => p.OutgoingStatus);
        }

    }
}
