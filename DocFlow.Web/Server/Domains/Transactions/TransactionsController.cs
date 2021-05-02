using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocFlow.Application.Transactions;
using DocFlow.Application.Transactions.Commands;
using DocFlow.Web.Server.Controllers;
using DocFlow.Web.Shared.Common;
using Microsoft.AspNetCore.Authorization;

namespace DocFlow.Web.Server.Domains.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTransactionsQuery query)
        {
            var result = await transactionService.BrowseTransactionsAsync(query);
            return Ok(result);
        }

        [HttpGet("initials")]
        public async Task<IActionResult> GetIntialTransactions()
        {
            var result = await transactionService.GetInitialTransactionsAsync();
            return Ok(result);
        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await transactionService.GetTransactionByIdAsync(Id);
            return Single(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            await transactionService.CreateTransactionAsync(command);
            return Created($"/api/transactions/{command.Id}", new EntityCreatedDto { Id = command.Id });
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateTransactionCommand command)
        {
            await transactionService.UpdateTransactionAsync(Id, command);
            return NoContent();
        }
    }
}
