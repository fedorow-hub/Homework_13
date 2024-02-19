
using Bank.Application.Accounts.Queries;
using Bank.Application.Accounts;
using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Bank.Application.Clients.Queries.GetClientList;

namespace Homework_13.ViewModels.Helpers
{
    public static class ViewModelHelper
    {
        public static async Task<AccountListVm> GetAccounts(Guid id)
        {
            var mediator = App.Host.Services.GetRequiredService<IMediator>();
            var query = new GetAccountsQuery
            {
                Id = id
            };
            var result = await mediator.Send(query);

            return result;
        }

        public static async Task<ClientListVm> GetAllClients()
        {
            var mediator = App.Host.Services.GetRequiredService<IMediator>();
            var query = new GetClientListQuery();
            var result = await mediator.Send(query);

            return result;
        }
    }
}
