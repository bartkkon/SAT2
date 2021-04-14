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
    /// Interaction logic for MonthlyQuantity.xaml
    /// </summary>
    public partial class MonthlyQuantity : Window
    {
        public string Items { get; set; }
        private string Revision;
        private decimal Year;
        private int Month;
        public MonthlyQuantity(string Items, string revision, decimal year, int month)
        {
            InitializeComponent();
            ((MonthlyQuantity_ViewModel)this.DataContext).Item = Items;
            ((MonthlyQuantity_ViewModel)this.DataContext).Revision = revision;
            ((MonthlyQuantity_ViewModel)this.DataContext).Year = year;
            ((MonthlyQuantity_ViewModel)this.DataContext).Month = month;
        }
    }
}
