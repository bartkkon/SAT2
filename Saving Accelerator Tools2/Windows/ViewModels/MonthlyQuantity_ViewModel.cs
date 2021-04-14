using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.Windows.ViewModels
{
    public class MonthlyQuantity_ViewModel : INotifyProperty
    {
        #region Constructors
        public MonthlyQuantity_ViewModel()
        {
            SaveButton = new RelayCommand<Window>(SaveData);
        }
        #endregion

        #region Private Variables
        private string item;
        private string revision;
        private decimal year;
        private int month;
        private string windowsTitle;
        private string tips;
        private string AddDataValue;
        private List<PNCList> DataGridCheck = new List<PNCList>();
        private string WrongData;
        #endregion

        #region Public Variables
        public string Item
        {
            get { return item; }
            set
            {
                item = value;
                WindowsTitle = $"Adding {item} Monthly";
                Tips = value;
            }
        }
        public string Revision
        {
            get { return revision; }
            set { revision = value; }
        }
        public decimal Year
        {
            get { return year; }
            set { year = value; }
        }
        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public string WindowsTitle
        {
            get { return windowsTitle; }
            set
            {
                windowsTitle = value;
                RisePropoertyChanged();
            }
        }
        public string Tips
        {
            get { return tips;}
            set
            {
                tips = $"Data Sequence: <{value}> TAB <Quantity>";
                RisePropoertyChanged();
            }
        }
        public string LoadData
        {
            get { return AddDataValue; }
            set
            {
                AddDataValue = value;
                ConvertData();
                RisePropoertyChanged();
            }
        }
        public List<PNCList> TableData
        {
            get { return DataGridCheck; }
            set
            {
                DataGridCheck = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand SaveButton
        {
            get; private set;
        }
        #endregion

        #region Functions
        private void SaveData(Window window)
        {
            if (item == "ANC")
            {
                if (ANC_Controller.CheckIfExist(year, "EA4", month))
                {
                    var MessageResults = MessageBox.Show($"Do you want replace current data for {month}/{year}", "Warining", MessageBoxButton.YesNo);
                    if (MessageResults == MessageBoxResult.Yes)
                    {
                        if (!ANC_Controller.RemoveRange(year, "EA4", month))
                        {
                            MessageBox.Show("Is not possible to remove data from DataBase", "Removing Problem", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                var NewList = new List<ANC_DB>();
                foreach (var Record in DataGridCheck)
                {
                    var NewRecord = new ANC_DB()
                    {
                        Item = Record.Item,
                        Quantity = Record.Quantity,
                        Revision = "EA4",
                        Month = month,
                        Year = year,
                    };
                    NewList.Add(NewRecord);
                }

                if (ANC_Controller.AddRange(NewList))
                {
                    MessageBox.Show("Updeted DataBase Finish with Succesfull!", "Data Updated!", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Updeted DataBase not updated Finish!", "Something go wrong!", MessageBoxButton.OK);
                    return;
                }

            }
            else if (item == "PNC")
            {
                if (PNC_Controller.CheckIfExist(year, "EA4", month))
                {

                    var MessageResults = MessageBox.Show($"Do you want replace current data for {month}/{year}", "Warining", MessageBoxButton.YesNo);
                    if (MessageResults == MessageBoxResult.Yes)
                    {
                        if (!PNC_Controller.RemoveRange(year, "EA4", month))
                        {
                            MessageBox.Show("Is not possible to remove data from DataBase", "Removing Problem", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                var NewList = new List<PNC_DB>();
                foreach (var Record in DataGridCheck)
                {
                    var NewRecord = new PNC_DB()
                    {
                        Item = Record.Item,
                        Quantity = Record.Quantity,
                        Revision = "EA4",
                        Month = month,
                        Year = year,
                    };
                    NewList.Add(NewRecord);
                }

                if (PNC_Controller.AddRange(NewList))
                {
                    MessageBox.Show("Updeted DataBase Finish with Succesfull!", "Data Updated!", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Updeted DataBase not updated Finish!", "Something go wrong!", MessageBoxButton.OK);
                    return;
                }
            }
            window.Close();
        }
        private void ConvertData()
        {
            int AddingRows = 0;
            int ZeroRows = 0;
            int ProblemRows = 0;
            DataGridCheck = new List<PNCList>();

            string[] LoadDataRows = AddDataValue.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (LoadDataRows.Length == 1 && LoadDataRows[0].Length < 9)
            {
                return;
            }
            if (Item == "PNC" && LoadDataRows[0].Remove(3) != "911" && LoadDataRows[0].Remove(3) != "999")
            {
                MessageBox.Show("Please check if you add correct Data.", "Wrong Data!", MessageBoxButton.OK);
                return;
            }
            else if (Item == "ANC" && LoadDataRows[0].Remove(3) == "911" && LoadDataRows[0].Remove(3) == "999")
            {
                MessageBox.Show("Please check if you add correct Data.", "Wrong Data!", MessageBoxButton.OK);
                return;
            }
            foreach (var Row in LoadDataRows)
            {
                if (string.IsNullOrEmpty(Row))
                {
                    continue;
                }
                string[] AddRow = Row.Split('\t');
                if (AddRow.Length != 2)
                {
                    continue;
                }

                if (decimal.TryParse(AddRow[1], out decimal Value))
                {
                    if (Value != 0)
                    {
                        var NewObject = new PNCList()
                        {
                            Item = AddRow[0],
                            Quantity = Value,
                        };
                        DataGridCheck.Add(NewObject);
                        AddingRows++;
                    }
                    else
                    {
                        ZeroRows++;
                    }
                }
                else
                {
                    WrongData += Environment.NewLine + $"{AddRow[0]} {AddRow[1]}";
                    ProblemRows++;
                }
            }

            MessageBox.Show($"Good Rows:\t\t{AddingRows}" + Environment.NewLine + $"Zero Rows:\t\t{ZeroRows}" + Environment.NewLine + $"Problematic Rows:\t\t{ProblemRows}", "Summary", MessageBoxButton.OK);
            if (ProblemRows != 0)
            {
                MessageBox.Show(WrongData, "Problematic Details", MessageBoxButton.OK);
            }
            TableData = DataGridCheck;
        }
        #endregion
    }

    public class PNCList
    {
        public string Item { get; set; }
        public decimal Quantity { get; set; }
    }
}
