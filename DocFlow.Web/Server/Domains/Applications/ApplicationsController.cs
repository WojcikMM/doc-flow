using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocFlow.Application.Applications;
using DocFlow.Application.Applications.Commands;
using DocFlow.Application.Applications.Queries;
using DocFlow.Web.Shared.Common;
using DocFlow.Web.Shared.Applications;
using DocFlow.Web.Server.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace DocFlow.Web.Server.Domains.Applications
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;
        private readonly string _userId = Infrastructure.EntityFramework.DataSeeder.TestUser1Id.ToString(); //TODO: Remove after identity implementation

        public ApplicationsController(IApplicationService applicationService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetApplicationQueryDto getApplicationQuery)
        {
            var query = mapper.Map<GetApplicationsQuery>(getApplicationQuery);
            var result = await applicationService.BrowseApplicationsAsync(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await applicationService.GetApplicationByIdAsync(Id);
            return Single(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterApplicationDto registerApplicationDto)
        {
            var command = mapper.Map<CreateApplicationCommand>(registerApplicationDto);
            command.RegistrationUser = _userId;

            await applicationService.CreateApplicationAsync(command);
            return Created($"/api/applications/{command.ApplicationId}", new EntityCreatedDto { Id = command.ApplicationId });
        }

        [HttpPatch("{Id}/assign")]
        public async Task<IActionResult> AssignCurrentUser([FromRoute] Guid Id)
        {
            return await AssignApplication(Id, _userId);
        }

        [HttpPatch("{Id}/assign/{UserId}")]
        public async Task<IActionResult> AssignApplication([FromRoute] Guid Id, [FromRoute] string UserId)
        {
            var command = new AssignUserHandlingCommand()
            {
                ApplicationId = Id,
                UserId = UserId
            };
            await applicationService.AssignUserHandling(command);
            return NoContent();
        }

        [HttpPatch("{Id}/release")]
        public async Task<IActionResult> ReleaseHandling([FromRoute] Guid Id)
        {
            var command = new ReleaseUserHandlingCommand()
            {
                ApplicationId = Id
            };

            await applicationService.ReleaseUserHandling(command);
            return NoContent();
        }

        [HttpPost("{Id}/apply/{TransactionId}")]
        public async Task<IActionResult> ApplyTransaction([FromRoute] Guid Id, [FromRoute] Guid TransactionId)
        {
            var command = new ApplyTransactionCommand()
            {
                ApplicationId = Id,
                TransactionId = TransactionId,
                UserId = _userId
            };

            await applicationService.ApplyTransaction(command);

            return NoContent();
        }

        [HttpPost("{Id}/transactions")]
        public async Task<IActionResult> GetAvaliableTransactions([FromRoute] Guid Id)
        {
            var transactions = await applicationService.GetAllowedTransactionsAsync(Id);
            return Ok(transactions);
        }
    }
}
