using Homework_13.Infrastructure.Commands.Base;
using Homework_13.ViewModels;
using Homework_13.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Homework_13.Infrastructure.Commands;

internal class ManageClientsCommand : Command
{
    private ClientInfoWindow _clientInfoWindow;
    public override bool CanExecute(object? parameter) => _clientInfoWindow == null;

    public override void Execute(object? parameter)
    {
        var window = new ClientInfoWindow 
        { 
            Owner = Application.Current.MainWindow
        };
        _clientInfoWindow = window;
        window.DataContext = App.Host.Services.GetRequiredService<ClientInfoViewModel>();
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        ((Window)sender!).Closed -= OnWindowClosed;
        _clientInfoWindow = null;
    }
}
