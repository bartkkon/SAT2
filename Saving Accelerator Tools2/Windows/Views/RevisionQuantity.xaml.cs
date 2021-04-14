using Saving_Accelerator_Tools2.Windows.ViewModels;
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
using System.Windows.Shapes;

namespace Saving_Accelerator_Tools2.Windows.Views
{
    /// <summary>
    /// Interaction logic for RevisionQuantity.xaml
    /// </summary>
    public partial class RevisionQuantity : Window
    {
        public RevisionQuantity(string Item, decimal Year, string Revision, int Month)
        {
            InitializeComponent();

            ((RevisionQuantity_ViewModel)this.DataContext).Item = Item;
            ((RevisionQuantity_ViewModel)this.DataContext).Revision = Revision;
            ((RevisionQuantity_ViewModel)this.DataContext).Year = Year;
            ((RevisionQuantity_ViewModel)this.DataContext).Month = Month;
        }
    }
}
