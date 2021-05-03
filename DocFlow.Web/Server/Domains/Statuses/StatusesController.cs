using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocFlow.Application.Statuses;
using DocFlow.Application.Statuses.Commands;
using DocFlow.Application.Statuses.Queries;
using DocFlow.Web.Server.Controllers;
using DocFlow.Web.Shared.Common;
using DocFlow.Web.Shared.Statuses;
using Microsoft.AspNetCore.Authorization;

namespace DocFlow.Web.Server.Domains.Statuses
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StatusesController : BaseController
    {
        private readonly IStatusesService statusesService;
        private readonly IMapper mapper;

        public StatusesController(IStatusesService statusesService, IMapper mapper)
        {
            this.statusesService = statusesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseStatuses([FromQuery] GetStatusesQueryDto getStatusesQuery)
        {
            var x = User.Identity.Name;
            var user = CurrentRequestUser;

            var query = mapper.Map<GetStatusesQuery>(getStatusesQuery);
            var result = await statusesService.BrowseStatusesAsync(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStatusById([FromRoute] Guid Id)
        {
            var status = await statusesService.GetStatusById(Id);
            return Single(status);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto createStatusDto)
        {
            var command = mapper.Map<CreateStatusCommand>(createStatusDto);
            await statusesService.CreateStatusAsync(command);
            return Created($"/api/statuses/{command.Id}", new EntityCreatedDto { Id = command.Id });
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid Id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            var command = mapper.Map<UpdateStatusCommand>(updateStatusDto);
            await statusesService.UpdateStatusAsync(Id, command);
            return NoContent();
        }
    }
}
