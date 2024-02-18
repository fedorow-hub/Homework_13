using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.Views.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для TransferToOtherClientWindow.xaml
    /// </summary>
    public partial class TransferToOtherClientWindow : Window
    {
        public TransferToOtherClientWindow()
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
