using MahApps.Metro.Controls.Dialogs;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Microsoft.Win32;
using System.IO;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System.Threading;
using System.Collections.ObjectModel;
using Saving_Accelerator_Tools2.Tasks;

namespace Saving_Accelerator_Tools2
{
    /// <summary>
    /// Interaction logic for AddingData.xaml
    /// </summary>
    public partial class AddingData : Window
    {
        private readonly decimal _year;
        private readonly int _month;
        private readonly string _revision;
        private readonly int _revisonStart;
        private readonly int _columns;
        private readonly string _configuration;
        private int _DeletedRecord;
        private int _NewRecord;
        private int _DuplicateRecord;
        private bool ErrorConditions = false;
        private bool STKAdd = false;
        private List<PNCListData> PNCExist;
        public Visibility TemplateButtonVisibility = Visibility.Hidden;


        public AddingData(decimal Year)
        {
            InitializeComponent();
            _year = Year;
            _columns = 4;
            STKAdd = true;

            this.Title = $"Adding Manualy STK for Year {_year}";
            InstructionBlock_Text.Text =
                    "------------------------------------------------" + Environment.NewLine +
                    "ANC <space> Description <space> IDCO <space> STK" + Environment.NewLine +
                    "------------------------------------------------";
        }

        public AddingData(string Configuration)
        {
            InitializeComponent();
            Mediator.Mediator.Register("AddingPNCList", NewList);
            _configuration = Configuration;
            TemplateButtonVisibility = Visibility.Visible;

            Mediator.Mediator.NotifyColleagues("Get_PNC_Data_Updated", null);
            do
            {
                Thread.Sleep(100);
            }
            while (PNCExist == null);

            Mediator.Mediator.Unregister("AddingPNCList", NewList);

            this.Title = "Adding PNC List";
            InstructionBlock_Text.Text = "---" + Environment.NewLine +
                "PNC" + Environment.NewLine +
                "---";

        }

        public AddingData()
        {
            if (_configuration == "PNCSpecial_ActionList")
            {
                this.Title = "Adding PNC List";
                InstructionBlock_Text.Text = "-----------------" + Environment.NewLine +
                "Use Template Copy" + Environment.NewLine +
                "-----------------";
            }
        }

        private void NewList(object NewList)
        {
            PNCExist = new List<PNCListData>();
            PNCExist = (NewList as ObservableCollection<PNCListData>).ToList();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_configuration == "PNC_ActionList")
            {
                Mouse.OverrideCursor = Cursors.Wait;
                AddPNCToActionList();
                Mouse.OverrideCursor = null;

                if (_DuplicateRecord != 0 || _NewRecord != 0)
                    MessageBox.Show("Completed:" + Environment.NewLine +
                        "Duplicated Record:    " + _DuplicateRecord.ToString() + Environment.NewLine +
                        "New Record:          " + _NewRecord.ToString(),
                        "Updated completed!");

                this.Close();
                return;
            }
            else if (_configuration == "PNCSpecial_ActionList")
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Mouse.OverrideCursor = null;
            }

            else if (STKAdd)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                var Status = AddSTKManualy();
                Mouse.OverrideCursor = null;

                if (Status)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }

        private void AddPNCToActionList()
        {
            string[] PNCList = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            
            if((PNCList[0].Trim()).Length !=9 || PNCList[0].Remove(3) != "911")
            {
                MessageBox.Show("Something is wrong with Data", "Error!");
                return;
            }

            if(PNCExist.Count() != 0)
            {
                var results = MessageBox.Show("Do you want replace all PNC List?", "Warning!", MessageBoxButton.YesNo);
                if(results == MessageBoxResult.Yes)
                {
                    PNCExist.Clear();
                }
            }
            foreach(var PNCNew in PNCList)
            {
                string PNCtoAdd = PNCNew.Trim();
                if (PNCtoAdd.Length == 9)
                {
                    if (PNCExist.Any(c => c.PNC == PNCtoAdd))
                    {
                        _DuplicateRecord++;
                    }
                    else
                    {
                        var NewRow = new PNCListData()
                        {
                            ID = 0,
                            PNC = PNCtoAdd,
                        };
                        PNCExist.Add(NewRow);
                        _NewRecord++;
                    }
                }
            }

            Mediator.Mediator.NotifyColleagues("Set_PNC_Data", PNCExist);
            _ = new Calculation_ActionView();
        }

        private bool AddSTKManualy()
        {
            List<STK_DB> STK_DataBase;
            DateTime UpdateData = new DateTime(DateTime.UtcNow.Year - 1000, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            string[] Data = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (Data.Count() < 1 || Data[0].Split(' ').Length < 4)
            {
                return false;
            }

            STK_DataBase = STK_Controller.Load_Year(_year).ToList();

            if (STK_DataBase.Count() != 0)
            {
                Mouse.OverrideCursor = null;
                var Result = MessageBox.Show($"Data exist for year: {_year}!" + Environment.NewLine + "Do you want replace it?", "Attention!!", MessageBoxButton.YesNo);
                Mouse.OverrideCursor = Cursors.Wait;

                if (Result == MessageBoxResult.Yes)
                {
                    STK_Controller.Delete_Year(_year);
                    STK_DataBase.Clear();
                }
                else
                {
                    return false;
                }
            }
            foreach (var Record in Data)
            {
                string[] RecordToAdd = Record.Split(' ');
                if (RecordToAdd.Length > 3)
                {
                    STK_DB NewBaseRecord = new STK_DB()
                    {
                        ANC = RecordToAdd[0],
                        Year = _year,
                        Date = UpdateData,
                    };

                    if (decimal.TryParse(RecordToAdd.Last(), out decimal STDRecults))
                    {
                        NewBaseRecord.STD = Math.Round(STDRecults, 4, MidpointRounding.AwayFromZero);
                    }
                    int RowLength = RecordToAdd.Length;

                    NewBaseRecord.IDCO = RecordToAdd[RowLength - 2];

                    if (RowLength > 4)
                    {
                        string DescriptionSum = string.Empty;
                        for (int count = 1; count < RowLength - 2; count++)
                        {
                            if (DescriptionSum != string.Empty)
                            {
                                DescriptionSum += " ";
                            }
                            DescriptionSum += RecordToAdd[count];
                        }
                        NewBaseRecord.Description = DescriptionSum.Trim();
                    }
                    else
                    {
                        NewBaseRecord.Description = RecordToAdd[1];
                    }

                    STK_DataBase.Add(NewBaseRecord);
                }
            }

            STK_Controller.Add_Year(STK_DataBase);
            return true;
            //}
            //return false;
        }

        private void TemplateButton_Click(object sender, RoutedEventArgs e)
        {
            string Template = @"\\PLWS4031\Project\CAD\Work\bartkkon\EC_Accelerator_Data\PNCSpec_Template.xlsm";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "PNCSpec_Template",
                DefaultExt = "Xlsm",
                Filter = "Excel Files (*.xlsm)|*xlsm",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                File.Copy(Template, saveFileDialog.FileName);
            }
        }
    }
}
