using Saving_Accelerator_Tools2.ViewModels;
using System;
using System.Collections.Generic;
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

namespace Saving_Accelerator_Tools2.Views
{
    /// <summary>
    /// Interaction logic for QuantityPage.xaml
    /// </summary>
    public partial class QuantityPage : Page
    {
        public QuantityPage(QuantityViewModel viewModel)
        {
            InitializeComponent();
            PNC.Checked += PNC_Checked;
            ANC.Checked += ANC_Checked;
            EA4.Checked += EA4_Checked;
            EA3.Checked += EA3_Checked;
            EA2.Checked += EA2_Checked;
            EA1.Checked += EA1_Checked;
            BU.Checked += BU_Checked;
        }

        private void PNC_Checked(object sender, RoutedEventArgs e)
        {
            if (ANC.IsChecked == true)
                ANC.IsChecked = false;
        }

        private void ANC_Checked(object sender, RoutedEventArgs e)
        {
            if (PNC.IsChecked == true)
                PNC.IsChecked = false;
        }

        private void EA4_Checked (object sender, RoutedEventArgs e)
        {
                EA3.IsChecked = false;
                EA2.IsChecked = false;
                EA1.IsChecked = false;
                BU.IsChecked = false;
        }

        private void EA3_Checked(object sender, RoutedEventArgs e)
        {
            EA4.IsChecked = false;
            EA2.IsChecked = false;
            EA1.IsChecked = false;
            BU.IsChecked = false;
        }

        private void EA2_Checked(object sender, RoutedEventArgs e)
        {
            EA4.IsChecked = false;
            EA3.IsChecked = false;
            EA1.IsChecked = false;
            BU.IsChecked = false;
        }

        private void EA1_Checked(object sender, RoutedEventArgs e)
        {
            EA4.IsChecked = false;
            EA3.IsChecked = false;
            EA2.IsChecked = false;
            BU.IsChecked = false;
        }

        private void BU_Checked(object sender, RoutedEventArgs e)
        {
            EA4.IsChecked = false;
            EA3.IsChecked = false;
            EA2.IsChecked = false;
            EA1.IsChecked = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
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
    }
}
