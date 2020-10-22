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
        private readonly List<string> RevisionList = new List<string>
        {
            "BU",
            "EA1",
            "EA2",
            "EA3",
        };

        public Quantity()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            Year_numeric.Value = DateTime.UtcNow.Year;
            Month_Numeric.Value = DateTime.UtcNow.Month;
            Revision_Combobox.ItemsSource = RevisionList;
            ANC_CheckBox.Checked += ANC_CheckBox_Checked;
        }

        private void ANC_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PNC_CheckBox.IsChecked = false;
        }

        private void PNC_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ANC_CheckBox.IsChecked = false;
        }

        private void Month_Button_Click(object sender, RoutedEventArgs e)
        {
            string _calculation = string.Empty;

            if (ANC_CheckBox.IsChecked == true)
                _calculation = "ANC";
            else if (PNC_CheckBox.IsChecked == true)
                _calculation = "PNC";

            if(_calculation != string.Empty)
            {
                var AddDataWindow = new AddingData(_calculation, (decimal)Year_numeric.Value, (int)Month_Numeric.Value);
                AddDataWindow.Show();
            }

        }

        private void Revision_Button_Click(object sender, RoutedEventArgs e)
        {
            string _calculation = string.Empty;

            if (ANC_CheckBox.IsChecked == true)
                _calculation = "ANC";
            else if (PNC_CheckBox.IsChecked == true)
                _calculation = "PNC";

            if (_calculation != string.Empty)
            {
                var AddDataWindow = new AddingData(_calculation, (decimal)Year_numeric.Value, Revision_Combobox.SelectedItem.ToString());
                AddDataWindow.Show();
            }
        }

        private void Year_numeric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (Year_numeric.Value > DateTime.UtcNow.Year)
                Revision_Combobox.SelectedIndex = 0;
            else if (Year_numeric.Value == DateTime.UtcNow.Year)
            {
                if (DateTime.UtcNow.Month >= 3 && DateTime.UtcNow.Month <= 5)
                    Revision_Combobox.SelectedIndex = 1;
                else if (DateTime.UtcNow.Month >= 6 && DateTime.UtcNow.Month <= 8)
                    Revision_Combobox.SelectedIndex = 2;
                else if (DateTime.UtcNow.Month >= 9 && DateTime.UtcNow.Month <= 12)
                    Revision_Combobox.SelectedIndex = 3;
                else
                    Revision_Combobox.SelectedIndex = 0;
            }
        }

    }
}
