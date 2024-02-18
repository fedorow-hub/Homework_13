using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.Views.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class AddAndWithdrawalDialogWindow : Window
    {
        public AddAndWithdrawalDialogWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
