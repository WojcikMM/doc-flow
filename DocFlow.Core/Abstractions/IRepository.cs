using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocFlow.Core.Abstractions
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
