using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocFlow.Application.Applications;
using DocFlow.Web.Server.Controllers;

namespace DocFlow.Web.Server.Domains.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingController : BaseController
    {
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;
        private readonly Guid _userId = Infrastructure.EntityFramework.DataSeeder.TestUser1Id; //TODO: Remove after identity implementation

        public HandlingController(IApplicationService applicationService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
        }

        [HttpGet("{Id}/can-handle")]
        public async Task<IActionResult> CanHandle([FromRoute]Guid Id)
        {
           var application = await applicationService.GetApplicationByIdAsync(Id);
            if(application.AssignedUserId != _userId)
            {
                return BadRequest("User is not assigned to this application.");
            }
            return Ok();
        }
    }
}
