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
    /// Interaction logic for PlatformView.xaml
    /// </summary>
    public partial class PlatformView : UserControl
    {
        public PlatformView()
        {
            InitializeComponent();
            DataContext = new PlatformViewModel();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FS.IsChecked = true;
            FI.IsChecked = true;
            BI.IsChecked = true;
            BU.IsChecked = true;
            FSBU.IsChecked = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FS.IsChecked = false;
            FI.IsChecked = false;
            BI.IsChecked = false;
            BU.IsChecked = false;
            FSBU.IsChecked = false;
        }

        private void CheckIfAll(object sender, RoutedEventArgs e)
        {
            if(FS.IsChecked == true && FI.IsChecked == true && BI.IsChecked == true && BU.IsChecked == true && FSBU.IsChecked == true)
            {
                All.IsChecked = true;
            }
            else
            {
                All.IsChecked = false;
            }
        }
    }
}
