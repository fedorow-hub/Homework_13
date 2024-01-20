using MediatR;

namespace Bank.Application.Bank.Queries
{
    public class GetBankQuery : IRequest<BankDetailVM>
    {
    }
}
