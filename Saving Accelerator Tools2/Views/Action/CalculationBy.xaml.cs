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
    /// Interaction logic for CalculationBy.xaml
    /// </summary>
    public partial class CalculationBy : UserControl
    {
        public CalculationBy()
        {
            InitializeComponent();
        }

        private void ANC_Checked(object sender, RoutedEventArgs e)
        {
            if (ANCS != null && PNC != null && PNCS != null)
            {
                ANCS.IsChecked = false;
                PNC.IsChecked = false;
                PNCS.IsChecked = false;
            }
        }
        private void ANCS_Checked(object sender, RoutedEventArgs e)
        {
            if (ANC != null && PNC != null && PNCS != null)
            {
                ANC.IsChecked = false;
                PNC.IsChecked = false;
                PNCS.IsChecked = false;
            }
        }
        private void PNC_Checked(object sender, RoutedEventArgs e)
        {
            if (ANCS != null && ANC != null && PNCS != null)
            {
                ANCS.IsChecked = false;
                ANC.IsChecked = false;
                PNCS.IsChecked = false;
            }
        }
        private void PNCS_Checked(object sender, RoutedEventArgs e)
        {
            if (ANCS != null && PNC != null && ANC != null)
            {
                ANCS.IsChecked = false;
                PNC.IsChecked = false;
                ANC.IsChecked = false;
            }
        }

    }
}
