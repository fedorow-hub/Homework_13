using Homework_13.ViewModels.DialogViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Homework_13.ViewModels.Base
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<LoginWindowViewModel>();
            services.AddTransient<OperationsWindowViewModel>();
            services.AddTransient<OpenAccountViewModel>();
            services.AddTransient<AccountInfoViewModel>();
            services.AddTransient<AddAndWithdrawalsViewModel>();
            services.AddTransient<AddDialogViewModel>();
            services.AddTransient<TransferBetweenOwnAccountsViewModel>();
            services.AddTransient<TransferBetweenOwnAccountsDialogViewModel>();
            services.AddTransient<TransferToOtherClientsAccountsViewModel>();
            services.AddTransient<TransferToOtherClientsDialogViewModel>();
            services.AddTransient<WithdrawalDialogViewModel>();

            return services;
        }
    }
}
