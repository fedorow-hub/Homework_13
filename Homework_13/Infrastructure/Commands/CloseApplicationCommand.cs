using Homework_13.Infrastructure.Commands.Base;
using System.Windows;

namespace Homework_13.Infrastructure.Commands;

public class CloseApplicationCommand : Command
{
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}
