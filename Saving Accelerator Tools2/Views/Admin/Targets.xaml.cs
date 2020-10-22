using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for Targets.xaml
    /// </summary>
    public partial class Targets : UserControl
    {
        private readonly List<string> RevisionList = new List<string>
        {
            "BU",
            "EA1",
            "EA2",
            "EA3",
            "EA4"
        };

        public Targets()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            Year_Num.Value = DateTime.UtcNow.Year;
            Revision_Combobox.ItemsSource = RevisionList;

            if (DateTime.UtcNow.Month >= 3 && DateTime.UtcNow.Month <= 5)
                Revision_Combobox.SelectedIndex = 1;
            else if (DateTime.UtcNow.Month >= 6 && DateTime.UtcNow.Month <= 8)
                Revision_Combobox.SelectedIndex = 2;
            else if (DateTime.UtcNow.Month >= 9 && DateTime.UtcNow.Month <= 12)
                Revision_Combobox.SelectedIndex = 3;
            else
                Revision_Combobox.SelectedIndex = 0;

            ImportData();

            CalcAllDevision();

            Year_Num.ValueChanged += Year_Num_ValueChanged;
            Revision_Combobox.SelectionChanged += Revision_ComboBox_SelectionChange;
        }

        private void ImportData()
        {
            var LoadData = Targets_Controller.Load((decimal)Year_Num.Value, (Revision_Combobox.SelectedItem as string));

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";

            DM_TextBox.Text = LoadData.DM.ToString("#,0", nfi);
            PC_TextBox.Text = LoadData.PC.ToString("#,0.0000", nfi);
            Electronic_TextBox.Text = LoadData.Electronic.ToString("#,0.0000", nfi);
            Mechanic_TextBox.Text = LoadData.Mechanic.ToString("#,0.0000", nfi);
            NVR_TextBox.Text = LoadData.NVR.ToString("#,0.0000", nfi);

            CalcAllDevision();
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (!Targets_Controller.Update(
                (decimal)Year_Num.Value,
                (Revision_Combobox.SelectedItem as string),
                decimal.Parse(DM_TextBox.Text),
                decimal.Parse(PC_TextBox.Text),
                decimal.Parse(Electronic_TextBox.Text),
                decimal.Parse(Mechanic_TextBox.Text),
                decimal.Parse(NVR_TextBox.Text)))
            {
                MessageBox.Show("Record not updated in Server for: " + Year_Num.Value.ToString() + " " + (Revision_Combobox.SelectedItem as string), "Error");
            }
            Mouse.OverrideCursor = null;
        }

        private void DM_KeyPress(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Tab)
            {/*Pozwala na wpisane wartości lub przeskoczenie do kolejnego elementu formy(tab)*/}
            else
                e.Handled = true;
        }

        private void Percent_KeyPress(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Tab)
            {/*Pozwala na wpisane wartości lub przeskoczenie do kolejnego elementu formy(tab)*/}
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal) // pozwala na pisanie przecinak jeden raz 
            {
                if (e.OriginalSource.ToString().IndexOf(',') != -1)
                    e.Handled = true;
                if ((e.Source as TextBox).SelectionStart == 0)
                    e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void TextBox_Leave(object sender, RoutedEventArgs e)
        {
            var LeaveBox = (sender as TextBox);
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";

            if (LeaveBox.Text == string.Empty)
            {
                LeaveBox.Text = "0";

            }
            else
            {
                if (decimal.TryParse(LeaveBox.Text, out decimal Value))
                {
                    if (LeaveBox.Name == "DM_TextBox")
                    {
                        Value = Math.Round(Value, 0, MidpointRounding.AwayFromZero);
                        LeaveBox.Text = Value.ToString("#,0", nfi);
                    }
                    else
                    {
                        Value = Math.Round(Value, 4, MidpointRounding.AwayFromZero);
                        LeaveBox.Text = Value.ToString("#,0.0000", nfi);
                    }
                }
                else
                {
                    LeaveBox.Text = "0";
                }
            }

            CalcAllDevision();
        }

        private void CalcAllDevision()
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            if (decimal.Parse(DM_TextBox.Text) != 0 && decimal.Parse(PC_TextBox.Text) != 0)
                PC_Calc_TextBlock.Text = Math.Round(decimal.Parse(DM_TextBox.Text) * (decimal.Parse(PC_TextBox.Text) / 100), 0, MidpointRounding.AwayFromZero).ToString("#,0", nfi);
            else
                PC_Calc_TextBlock.Text = string.Empty;
            if (decimal.Parse(DM_TextBox.Text) != 0 && decimal.Parse(Electronic_TextBox.Text) != 0)
                Electronic_Calc_TextBlock.Text = Math.Round(decimal.Parse(DM_TextBox.Text) * (decimal.Parse(Electronic_TextBox.Text) / 100), 0, MidpointRounding.AwayFromZero).ToString("#,0", nfi);
            else
                Electronic_Calc_TextBlock.Text = string.Empty;
            if (decimal.Parse(DM_TextBox.Text) != 0 && decimal.Parse(Mechanic_TextBox.Text) != 0)
                Mechanic_Calc_TextBlock.Text = Math.Round(decimal.Parse(DM_TextBox.Text) * (decimal.Parse(Mechanic_TextBox.Text) / 100), 0, MidpointRounding.AwayFromZero).ToString("#,0", nfi);
            else
                Mechanic_Calc_TextBlock.Text = string.Empty;
            if (decimal.Parse(DM_TextBox.Text) != 0 && decimal.Parse(NVR_TextBox.Text) != 0)
                NVR_Calc_TextBlock.Text = Math.Round(decimal.Parse(DM_TextBox.Text) * (decimal.Parse(NVR_TextBox.Text) / 100), 0, MidpointRounding.AwayFromZero).ToString("#,0", nfi);
            else
                NVR_Calc_TextBlock.Text = string.Empty;
        }

        private void TextBox_Entry(object sender, RoutedEventArgs e)
        {
            TextBox EntryBox = (sender as TextBox);

            if (decimal.Parse(EntryBox.Text) == 0)
            {
                EntryBox.Text = string.Empty;
                EntryBox.SelectionStart = 0;
            }
        }

        private void Year_Num_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            ImportData();
            Mouse.OverrideCursor = null;
        }

        private void Revision_ComboBox_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            ImportData();
            Mouse.OverrideCursor = null;
        }
    }
}
