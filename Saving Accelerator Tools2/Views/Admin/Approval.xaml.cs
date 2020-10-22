using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
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
    /// Interaction logic for Approval.xaml
    /// </summary>
    public partial class Approval : UserControl
    {
        private List<Approvals_DB> ApproveYear;
        ComboBoxItem NoData = new ComboBoxItem()
        {
            Content = "No Data",

        };
        ComboBoxItem DuringCalc = new ComboBoxItem()
        {
            Content = "During Calc",
        };
        ComboBoxItem Approve = new ComboBoxItem()
        {
            Content = "Approve",
        };


        public Approval()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            Year_Numeric.Value = DateTime.UtcNow.Year;
            Month_Numeric.Value = DateTime.UtcNow.Month;

            List<ComboBoxItem> ItemComboBox = new List<ComboBoxItem>
            {
                NoData,
                DuringCalc,
                Approve
            };

            BU_Status_Combobox.ItemsSource = ItemComboBox;
            EA1_Status_Combobox.ItemsSource = ItemComboBox;
            EA2_Status_Combobox.ItemsSource = ItemComboBox;
            EA3_Status_Combobox.ItemsSource = ItemComboBox;
            Month_Status_Combobox.ItemsSource = ItemComboBox;
            Electronic_Status_Combobox.ItemsSource = ItemComboBox;
            Mechanic_Status_Combobox.ItemsSource = ItemComboBox;
            NVR_Status_Combobox.ItemsSource = ItemComboBox;

            LoadYear();

            Year_Numeric.ValueChanged += Year_numeric_ValueChange;
            Month_Numeric.ValueChanged += Month_Numeric_ValueChange;
        }

        private void LoadYear()
        {
            if (ApproveYear != null)
                ApproveYear.Clear();

            ApproveYear = Approvals_Controller.LoadYear((decimal)Year_Numeric.Value).ToList();

            BU_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Revision == "BU").First().Status;
            EA1_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Revision == "EA1").First().Status;
            EA2_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Revision == "EA2").First().Status;
            EA3_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Revision == "EA3").First().Status;
            Electronic_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Devision == "Electronic").First().Status;
            Mechanic_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Devision == "Mechanic").First().Status;
            NVR_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Devision == "NVR").First().Status;
            Month_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Month == Month_Numeric.Value).First().Status;
        }

        private void SaveYear()
        {
            var context = new DataBaseConnetionContext();

            ApproveYear.Where(u => u.Revision == "BU").First().Status = BU_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA1").First().Status = EA1_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA2").First().Status = EA2_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA3").First().Status = EA3_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "Electronic").First().Status = Electronic_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "Mechanic").First().Status = Mechanic_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "NVR").First().Status = NVR_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Month == Month_Numeric.Value).First().Status = Month_Status_Combobox.SelectedIndex;

            foreach (var Approval in ApproveYear)
            {
                context.Approvals.Update(Approval);
            }
            context.SaveChanges();
        }

        private void CheckChange(decimal OldYear)
        {
            List<Approvals_DB> BaseList = new List<Approvals_DB>();
            BaseList = Approvals_Controller.LoadYear(OldYear).ToList();
            bool Change = false;

            ApproveYear.Where(u => u.Revision == "BU").First().Status = BU_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA1").First().Status = EA1_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA2").First().Status = EA2_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Revision == "EA3").First().Status = EA3_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "Electronic").First().Status = Electronic_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "Mechanic").First().Status = Mechanic_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Devision == "NVR").First().Status = NVR_Status_Combobox.SelectedIndex;
            ApproveYear.Where(u => u.Month == Month_Numeric.Value).First().Status = Month_Status_Combobox.SelectedIndex;

            foreach (var Memory in ApproveYear)
            {
                if(Memory.Status !=  BaseList.Where(u => u.ID == Memory.ID).First().Status)
                {
                    Change = true;
                }
            }

            if(Change)
            {
                var Results = MessageBox.Show("Do you wnat save change For Year " + OldYear.ToString(), "Warning!!", MessageBoxButton.YesNo);
                if(Results == MessageBoxResult.Yes)
                {
                    SaveYear();
                }
            }

        }

        private void Year_numeric_ValueChange(object sender, RoutedPropertyChangedEventArgs<double?> eventArgs)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            CheckChange((decimal)eventArgs.OldValue);
            LoadYear();
            Mouse.OverrideCursor = null;
        }

        private void Month_Numeric_ValueChange(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ApproveYear.Where(u => u.Month == e.OldValue).First().Status = Month_Status_Combobox.SelectedIndex;
            Month_Status_Combobox.SelectedIndex = ApproveYear.Where(u => u.Month == e.NewValue).First().Status;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            SaveYear();
            Mouse.OverrideCursor = null;
        }
    }
}
