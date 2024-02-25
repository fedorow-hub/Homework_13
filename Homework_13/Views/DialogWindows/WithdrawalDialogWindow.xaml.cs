using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;

namespace Homework_13.Views.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для WithdrawalDialogWindow.xaml
    /// </summary>
    public partial class WithdrawalDialogWindow : Window
    {
        public WithdrawalDialogWindow()
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
