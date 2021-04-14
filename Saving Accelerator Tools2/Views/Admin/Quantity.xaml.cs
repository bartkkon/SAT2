using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
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

namespace Saving_Accelerator_Tools2.Views.Admin
{
    /// <summary>
    /// Interaction logic for Quantity.xaml
    /// </summary>
    public partial class Quantity : UserControl
    {

        public Quantity()
        {
            InitializeComponent();
        }

        private void ANC_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (PNC_CheckBox.IsChecked == true)
                PNC_CheckBox.IsChecked = false;
        }

        private void PNC_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ANC_CheckBox.IsChecked == true)
                ANC_CheckBox.IsChecked = false;
        }
    }
}
