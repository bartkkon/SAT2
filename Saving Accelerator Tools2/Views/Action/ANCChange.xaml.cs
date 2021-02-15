using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Saving_Accelerator_Tools2.Views.Action
{
    /// <summary>
    /// Interaction logic for ANCChange.xaml
    /// </summary>
    public partial class ANCChange : UserControl
    {
        public ANCChange()
        {
            InitializeComponent();
            //DataContext = new ANCChangeViewModel();
        }

        private void ANC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                return;
            }
            if (e.Key != Key.A && !(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            else if (e.Key == Key.A && (e.Source as TextBox).SelectionStart != 0)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.A && (e.Source as TextBox).SelectionStart == 0)
            {
                if ((e.Source as TextBox).Text != string.Empty || int.TryParse((e.Source as TextBox).Text, out _))
                {
                    e.Handled = true;
                }
            }
        }

        private void Quantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                return;
            if (e.Key != Key.OemPeriod && e.Key != Key.Decimal && !(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && (e.Source as TextBox).Text.Count(f => f == ',') != 0)
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && (e.Source as TextBox).SelectionStart == 0)
            {
                (e.Source as TextBox).Text = "0" + (e.Source as TextBox).Text;
                (e.Source as TextBox).SelectionStart = 1;
            }
        }

        private void Color_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            (sender as TextBlock).Foreground = Brushes.Black;
            (sender as TextBlock).Text = (sender as TextBlock).Text.Replace(".", ",");
            if (decimal.TryParse((sender as TextBlock).Text, out decimal result))
            {
                if (result > 0)
                    (sender as TextBlock).Foreground = Brushes.Lime;
                else if (result < 0)
                    (sender as TextBlock).Foreground = Brushes.Red;
            }
        }

        private void Cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                return;
            if (e.Key != Key.OemPeriod &&
                e.Key != Key.Decimal &&
                e.Key != Key.OemMinus &&
                e.Key != Key.Subtract &&
                !(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && (e.Source as TextBox).Text.Count(f => f == '.') != 0)
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemMinus || e.Key == Key.Subtract) && (e.Source as TextBox).Text.Count(f => f == '-') != 0)
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && (e.Source as TextBox).SelectionStart == 0)
            {
                (e.Source as TextBox).Text = "0" + (e.Source as TextBox).Text;
                (e.Source as TextBox).SelectionStart = 1;
            }
            else if ((e.Key == Key.OemMinus || e.Key == Key.Subtract) && (e.Source as TextBox).SelectionStart != 0)
            {
                e.Handled = true;
            }
        }

        private void Percent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                return;
            if (e.Key != Key.OemComma && e.Key != Key.Decimal && !(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemComma || e.Key == Key.Decimal) && (e.Source as TextBox).Text.Count(f => f == ',') != 0)
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.OemComma || e.Key == Key.Decimal) && (e.Source as TextBox).SelectionStart == 0)
            {
                (e.Source as TextBox).Text = "0" + (e.Source as TextBox).Text;
                (e.Source as TextBox).SelectionStart = 1;
            }
        }

        private void Percent_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == string.Empty)
            {
                (sender as TextBox).Text = "100";
            }
        }

        private void Estimation1_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            //SumEstymationAll();
            //GeneretedCalculationValue();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
            (sender as TextBox).SelectAll();
        }
    }
}
