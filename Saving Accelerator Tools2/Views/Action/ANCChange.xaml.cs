using Saving_Accelerator_Tools2.ViewModels.Action;
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
            DataContext = new ANCChangeViewModel();
        }

        private void ANC_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                return;
            }
            if (e.Key != Key.A && !char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
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

        private void ANC_BorderCheck(object sender, EventArgs eventArgs)
        {
            if((sender as TextBox).Text.Length < 9 && (sender as TextBox).Text.Length >0)
            {
                //(sender as TextBox).va
            }
            else
            {
                //(sender as TextBox).BorderBrush = Brushes.;
            }
        }

        private void Quantity_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Delta_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Cost_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void Percent_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
