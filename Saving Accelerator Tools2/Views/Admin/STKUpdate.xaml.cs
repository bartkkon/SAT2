using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.Core.Update;
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

namespace Saving_Accelerator_Tools2.Views.Admin
{
    /// <summary>
    /// Interaction logic for STKUpdate.xaml
    /// </summary>
    public partial class STKUpdate : UserControl
    {

        public STKUpdate()
        {
            InitializeComponent();
        }

        private void STK_Normal_Buttom_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var Results = new STK_DataBaseUpdate().StandardUpdate();
            Mouse.OverrideCursor = null;
            if (Results[0] != -1)
                MessageBox.Show("Check Records: " + Results[0] + Environment.NewLine +
                    "Updated Records: " + Results[1] + Environment.NewLine +
                    "New Records: " + Results[2], "STK Updated");
            else
                MessageBox.Show("Problem with find file, more than 30 was not genereted!", "Error");
        }

    }
}
