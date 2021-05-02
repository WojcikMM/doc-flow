using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocFlow.Application.Statuses;
using DocFlow.Application.Statuses.Commands;
using DocFlow.Application.Statuses.DTOs;
using DocFlow.Application.Statuses.Queries;
using DocFlow.Core.Domain;
using DocFlow.Core.Exceptions;
using DocFlow.Core.Repositories;

namespace DocFlow.Infrastructure.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly IStatusesRepository repository;
        private readonly IMapper mapper;

        public StatusesService(IStatusesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<StatusDto>> BrowseStatusesAsync(GetStatusesQuery query)
        {
            var statuses = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<StatusDto>>(statuses);
        }

        public async Task CreateStatusAsync(CreateStatusCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                throw new AggregateValidationException("Status Id is invalid.");
            }

            var status = await repository.GetAsync(command.Id);

            if (status != null)
            {
                throw new AggregateIllegalLogicException("Cannot create status with this Id. Status exists.");
            }

            await repository.CreateAsync(new StatusEntity(command.Id, command.Name));
        }

        public async Task<StatusDto> GetStatusById(Guid id)
        {
            var status = await GetStatusAsync(id);
            return mapper.Map<StatusDto>(status);
        }

        public async Task UpdateStatusAsync(Guid Id, UpdateStatusCommand command)
        {
            var status = await GetStatusAsync(Id);

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                status.SetName(command.Name);
            }
        }

        private async Task<StatusEntity> GetStatusAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new AggregateValidationException("Status Id is invalid.");
            }
            var status = await repository.GetAsync(id);

            if (status == null)
            {
                throw new AggregateNotFoundException("Status with given Id not exists.");
            }
            return status;
        }
    }
}
