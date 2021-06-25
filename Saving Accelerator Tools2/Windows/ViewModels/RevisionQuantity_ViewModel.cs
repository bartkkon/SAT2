using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.Windows.ViewModels
{
    public class RevisionQuantity_ViewModel //: INotifyProperty
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
                //RisePropoertyChanged();
            }
        }
        public string TextBoxData
        {
            get { return textBoxData; }
            set
            {
                textBoxData = value;
                ConvertData();
                //RisePropoertyChanged();
            }
        }
        public List<ItemList> TableData
        {
            get { return tables; }
            set
            {
                tables = value;
                //RisePropoertyChanged();
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
                        if (!PNC_Controller.RemoveRange(year, Revision, 0))
                        {
                            MessageBox.Show("Is not possible to remove data from DataBase", "Removing Problem", MessageBoxButton.OK);
                        }
                        if (PNCTotally_Controler.CheckIfExist(Year, Revision, 0))
                        {
                            if (!PNCTotally_Controler.RemoveRange(year, Revision, 0))
                            {
                                MessageBox.Show("Is not possible to remove data from DataBase (PNC Totality)", "Removing Problem", MessageBoxButton.OK);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                var NewList = new List<PNC_DB>();
                var NewTotalyList = new List<PNCTotality_DB>();
                PNCTotalityRecords(NewTotalyList);

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
                        {
                            NewList.Add(NewRecord);
                            PNCTotalityAdd(NewTotalyList, NewRecord.Item, NewRecord.Quantity, NewRecord.Month);
                        }
                    }
                }

                if (PNC_Controller.AddRange(NewList) && PNCTotally_Controler.AddRange(NewTotalyList))
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
        private void PNCTotalityRecords(List<PNCTotality_DB> List)
        {
            int StartMonth = revision switch
            {
                "BU" => 1,
                "EA1" => 3,
                "EA2" => 6,
                "EA3" => 9,
                _ => 13,
            };


            for (int monthFor = StartMonth; monthFor <= 12; monthFor++)
            {
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "DMD", Instalation = "FS" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "DMD", Instalation = "FI" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "DMD", Instalation = "BI" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "DMD", Instalation = "FSBU" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "D45", Instalation = "FS" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "D45", Instalation = "FI" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "D45", Instalation = "BI" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "D45", Instalation = "FSBU" });
                List.Add(new PNCTotality_DB() { Revision = revision, Year = year, Month = monthFor, Structure = "Proxy", Instalation = null }); 
            }
        }
        private void PNCTotalityAdd(List<PNCTotality_DB> List, string PNC, decimal Value, int monthAdd)
        {
            string Structure = PNC.Remove(3) == "999" ? "Proxy" : PNC.Remove(4).Remove(0, 3) == "5" ? "DMD" : PNC.Remove(4).Remove(0, 3) == "0" ? "D45" : string.Empty;
            string Installation = string.Empty;

            if (Structure == "DMD")
            {
                Installation = PNC.Remove(5).Remove(0, 4) switch
                {
                    "1" => "FS",
                    "2" => "BI",
                    "3" => "FI",
                    "4" => "FSBU",
                    _ => "Empty",
                };
            }
            else if (Structure == "D45")
            {
                Installation = PNC.Remove(5).Remove(0, 4) switch
                {
                    "5" => "FS",
                    "6" => "BI",
                    "7" => "FI",
                    "8" => "FSBU",
                    _ => "Empty",
                };
            }

            var ListRecord = Structure == "Proxy" ? List.Where(record => record.Structure == Structure && record.Month == monthAdd).FirstOrDefault() : List.Where(record => record.Structure == Structure && record.Instalation == Installation && record.Month == monthAdd).FirstOrDefault();
            if (ListRecord != null)
            {
                ListRecord.Quantity += Value;
            }
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
