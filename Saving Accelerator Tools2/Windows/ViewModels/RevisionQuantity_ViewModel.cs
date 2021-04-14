﻿using Saving_Accelerator_Tools2.Contracts.Services;
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
    public class RevisionQuantity_ViewModel : INotifyProperty
    {
        #region Constructors
        public RevisionQuantity_ViewModel()
        {
            SaveButton = new RelayCommand<Window>(Save);
        }
        #endregion

        #region private Variables
        private string item;
        private decimal year;
        private string revision;
        private int month;

        private int monthstart;
        private int columnsCheck;

        private string title;
        private string tips;

        private string textBoxData;
        private List<ItemList> tables = new List<ItemList>();
        #endregion

        #region public Variables
        public string Item
        {
            get { return item; }
            set
            {
                item = value;
                WindowsTitle = $"Adding {item} data for Revision {revision}";
            }
        }
        public decimal Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Revision
        {
            get { return revision; }
            set
            {
                revision = value;
                RevisionMonthstart();

            }
        }
        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public string WindowsTitle
        {
            get { return title; }
            set
            {
                title = value;
                RisePropoertyChanged();
            }
        }
        public string TextBoxData
        {
            get { return textBoxData; }
            set
            {
                textBoxData = value;
                ConvertData();
                RisePropoertyChanged();
            }
        }
        public List<ItemList> TableData
        {
            get { return tables; }
            set
            {
                tables = value;
                RisePropoertyChanged();
            }
        }

        #endregion

        #region Functions
        private void RevisionMonthstart()
        {
            monthstart = revision switch
            {
                "BU" => 1,
                "EA1" => 3,
                "EA2" => 6,
                "EA3" => 9,
                _ => 13,
            };
            columnsCheck = revision switch
            {
                "BU" => 13,
                "EA1" => 11,
                "EA2" => 8,
                "EA3" => 5,
                _ => 0,
            };
        }
        private void ConvertData()
        {
            TableData = new List<ItemList>();

            var DataRows = textBoxData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (DataRows.Length == 1 && DataRows[0].Length < 9)
            {
                return;
            }
            if (item == "PNC" && DataRows[0].Remove(3) != "911" && DataRows[0].Remove(3) != "999")
            {
                MessageBox.Show("Please check if you add correct Data.", "Wrong Data!", MessageBoxButton.OK);
                return;
            }
            else if (item == "ANC" && DataRows[0].Remove(3) == "911" && DataRows[0].Remove(3) == "999")
            {
                MessageBox.Show("Please check if you add correct Data.", "Wrong Data!", MessageBoxButton.OK);
                return;
            }

            foreach (var DataRow in DataRows)
            {
                if (string.IsNullOrEmpty(DataRow))
                {
                    continue;
                }
                string[] SeparateRow = DataRow.Split('\t');
                if (SeparateRow.Length != columnsCheck)
                {
                    MessageBox.Show($"For Revision {revision}, current data is incorrect!\nWrong number of column:\nShould be:\t\t {columnsCheck}\nIs:\t\t\t {SeparateRow.Length}", "Problems!", MessageBoxButton.OK);
                    return;
                }

                var NewRecord = new ItemList()
                {
                    Item = SeparateRow[0],
                };

                for (int counter = 1; counter <= 12 - monthstart + 1; counter++)
                {
                    if (decimal.TryParse(SeparateRow[counter], out decimal Value))
                    {
                        if (Value != 0)
                        {
                            var MonthName = typeof(ItemList).GetProperty(Months.Convert(counter + monthstart - 1));
                            MonthName.SetValue(NewRecord, Value);
                        }
                    }
                }
                tables.Add(NewRecord);
            }
            TableData = tables;
        }

        #endregion

        #region Buttons
        public ICommand SaveButton
        {
            get; private set;
        }
        #endregion

        #region Buttons Functions
        private void Save(Window windows)
        {
            if(item == "ANC")
            {
                if (ANC_Controller.CheckIfExist(Year, Revision, 0))
                {
                    var MessageResults = MessageBox.Show($"Do you want replace current data for {month}/{year}", "Warining", MessageBoxButton.YesNo);
                    if (MessageResults == MessageBoxResult.Yes)
                    {
                        if (!ANC_Controller.RemoveRange(year, Revision, 0))
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
                foreach(var Line in tables)
                {
                    for (int counter = monthstart; counter<=12; counter++)
                    {
                        var NewRecord = new ANC_DB()
                        {
                            Item = Line.Item,
                            Revision = revision,
                            Month = counter,
                            Year = year,
                        };
                        var MonthName = typeof(ItemList).GetProperty(Months.Convert(counter));
                        NewRecord.Quantity = (decimal)MonthName.GetValue(Line);

                        if (NewRecord.Quantity != 0)
                            NewList.Add(NewRecord);
                    }
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
            else if(item == "PNC")
            {
                if (PNC_Controller.CheckIfExist(Year, Revision, 0))
                {
                    var MessageResults = MessageBox.Show($"Do you want replace current data for {month}/{year}", "Warining", MessageBoxButton.YesNo);
                    if (MessageResults == MessageBoxResult.Yes)
                    {
                        if (!ANC_Controller.RemoveRange(year, Revision, 0))
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
                foreach (var Line in tables)
                {
                    for (int counter = monthstart; counter <= 12; counter++)
                    {
                        var NewRecord = new PNC_DB()
                        {
                            Item = Line.Item,
                            Revision = revision,
                            Month = counter,
                            Year = year,
                        };
                        var MonthName = typeof(ItemList).GetProperty(Months.Convert(counter));
                        NewRecord.Quantity = (decimal)MonthName.GetValue(Line);

                        if (NewRecord.Quantity != 0)
                            NewList.Add(NewRecord);
                    }
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
            windows.Close();
        }
        #endregion
    }

    public class ItemList
    {
        public string Item { get; set; }
        public decimal January { get; set; }
        public decimal Febuary { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }
    }

    public class Months
    {
        public static string Convert(int Month)
        {
            return Month switch
            {
                1 => "January",
                2 => "Febuary",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "",
            };
        }

        public static int Convert(string Month)
        {
            return Month switch
            {
                "January" => 1,
                "Febuary" => 2,
                "March" => 3,
                "April" => 4,
                "May" => 5,
                "June" => 6,
                "July" => 7,
                "August" => 8,
                "September" => 9,
                "October" => 10,
                "November" => 11,
                "December" => 12,
                _ => 13,
            };
        }
    }
}
