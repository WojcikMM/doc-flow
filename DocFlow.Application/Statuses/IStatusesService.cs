using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocFlow.Application.Statuses.Commands;
using DocFlow.Application.Statuses.DTOs;
using DocFlow.Application.Statuses.Queries;

namespace DocFlow.Application.Statuses
{
    public interface IStatusesService
    {
        public Task<IEnumerable<StatusDto>> BrowseStatusesAsync(GetStatusesQuery query);
        public Task<StatusDto> GetStatusById(Guid id);
        public Task CreateStatusAsync(CreateStatusCommand command);
        public Task UpdateStatusAsync(Guid Id, UpdateStatusCommand command);
    }
}
