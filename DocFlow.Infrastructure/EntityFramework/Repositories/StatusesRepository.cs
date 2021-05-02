using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocFlow.Core.Domain;
using DocFlow.Core.Repositories;

namespace DocFlow.Infrastructure.EntityFramework.Repositories
{
    public class StatusesRepository : IStatusesRepository
    {
        private readonly DocFlowDbContext unitOfWork;

        public StatusesRepository(DocFlowDbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(StatusEntity entity)
        {
            unitOfWork.Add(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<StatusEntity>> GetAllAsync()
        {
            return await AllEntities().ToListAsync();
        }

        public async Task<StatusEntity> GetAsync(Guid id)
        {
            return await AllEntities().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(StatusEntity entity)
        {
            unitOfWork.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        private IQueryable<StatusEntity> AllEntities()
        {
            return unitOfWork.Statuses.Include(p => p.AvailableTransactions).Include(p => p.IncomingTransactions).Include(p => p.ApplicationsWithStatus);
        }
    }
}
