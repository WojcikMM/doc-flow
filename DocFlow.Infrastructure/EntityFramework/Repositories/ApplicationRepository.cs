using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocFlow.Core.Domain;
using DocFlow.Core.Repositories;

namespace DocFlow.Infrastructure.EntityFramework.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DocFlowDbContext unitOfWork;

        public ApplicationRepository(DocFlowDbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(ApplicationEntity entity)
        {
            unitOfWork.Applications.Add(entity);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationEntity>> GetAllAsync()
        {
            return await Entities().ToListAsync();
        }

        public async Task<ApplicationEntity> GetAsync(Guid id)
        {
            return await Entities().FirstOrDefaultAsync(application => application.Id == id);
        }

        public async Task UpdateAsync(ApplicationEntity entity)
        {
            unitOfWork.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        private IQueryable<ApplicationEntity> Entities()
        {
            return unitOfWork.Applications.Include(p => p.TransactionItems).Include(p => p.User);
        }
    }
}
