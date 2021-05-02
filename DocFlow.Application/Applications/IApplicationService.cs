using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocFlow.Application.Applications.Commands;
using DocFlow.Application.Applications.DTOs;
using DocFlow.Application.Applications.Queries;
using DocFlow.Application.Transactions.DTOs;

namespace DocFlow.Application.Applications
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> BrowseApplicationsAsync(GetApplicationsQuery query);
        Task<ApplicationDto> GetApplicationByIdAsync(Guid id);
        Task<IEnumerable<TransactionDto>> GetAllowedTransactionsAsync(Guid applicationId);
        Task CreateApplicationAsync(CreateApplicationCommand command);
        Task ApplyTransaction(ApplyTransactionCommand command);
        Task AssignUserHandling(AssignUserHandlingCommand command);
        Task ReleaseUserHandling(ReleaseUserHandlingCommand command);
    }
}
