using MahApps.Metro.Controls.Dialogs;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
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
        private int _UpdateRecord;
        private int _NewRecord;
        private decimal[] Proxy = new decimal[12];
        private decimal[] DMD_FI = new decimal[12];
        private decimal[] DMD_FS = new decimal[12];
        private decimal[] DMD_BI = new decimal[12];
        private decimal[] DMD_FSBU = new decimal[12];
        private decimal[] D45_FI = new decimal[12];
        private decimal[] D45_FS = new decimal[12];
        private decimal[] D45_BI = new decimal[12];
        private decimal[] D45_FSBU = new decimal[12];

        public AddingData(string Configuration, decimal Year, int Month)
        {
            InitializeComponent();
            _year = Year;
            _month = Month;
            _configuration = Configuration;

            if (Configuration == "ANC")
            {
                _columns = 2;
                this.Title = "Adding Monthly Data => ANC";
                InstructionBlock_Text.Text = "------------------------" + Environment.NewLine +
                    "ANC    <space>    Quantity" + Environment.NewLine +
                    "------------------------";
            }
            else if (Configuration == "PNC")
            {
                _columns = 2;
                this.Title = "Adding Monthly Data => PNC";
                InstructionBlock_Text.Text = "------------------------" + Environment.NewLine +
                    "PNC    <space>    Quantity" + Environment.NewLine +
                    "------------------------";
            }
        }

        public AddingData(string Configuration, decimal Year, string Revision)
        {
            InitializeComponent();
            _year = Year;
            _configuration = Configuration;
            _revision = Revision;

            if (Revision == "BU")
            {
                _columns = 13;
                _revisonStart = 1;
            }
            else if (Revision == "EA1")
            {
                _columns = 11;
                _revisonStart = 3;
            }
            else if (Revision == "EA2")
            {
                _columns = 7;
                _revisonStart = 6;
            }
            else if (Revision == "EA3")
            {
                _columns = 5;
                _revisonStart = 9;
            }
            else
            {
                _columns = 0;
                _revisonStart = 13;
            }


            if (Configuration == "ANC")
            {
                this.Title = "Adding Revision " + Revision + " Data => ANC";
                InstructionBlock_Text.Text = "----------------------------------------------------------------------" + Environment.NewLine +
                    "ANC <space> Quantity1 <space> Quantity2 <space> ... <space> Quantity12" + Environment.NewLine +
                    "--------------------------";
            }
            else if (Configuration == "PNC")
            {
                this.Title = "Adding Revision " + Revision + "  Data => PNC";
                InstructionBlock_Text.Text = "----------------------------------------------------------------------" + Environment.NewLine +
                    "PNC <space> Quantity1 <space> Quantity2 <space> ... <space> Quantity12" + Environment.NewLine +
                    "----------------------------------------------------------------------";
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (_revision == null)
            {
                if (_configuration == "ANC")
                    UpdateDataToDataBase_ANCMonth();
                else if (_configuration == "PNC")
                    UpdateDataToDataBase_PNCMonth();
            }
            else
            {
                if (_configuration == "ANC")
                    UpdateDataToDataBase_ANCRevision();
                else if (_configuration == "PNC")
                    UpdateDataToDataBase_PNCRevision();
            }
            Mouse.OverrideCursor = null;

            if (_UpdateRecord != 0 && _NewRecord != 0)
                MessageBox.Show("Completed:" + Environment.NewLine +
                    "Updateded Record:    " + _UpdateRecord.ToString() + Environment.NewLine +
                    "New Record:          " + _NewRecord.ToString(),
                    "Updated completed!");

            this.Close();
        }

        private bool CheckError_QuantityColum(string Row)
        {//Sprawdzenie czy wprowadzone dane są w odpowiedniej ilości kolumn
            if (Row.Split(' ').Length != _columns)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Wrong numer of columns!!!", "Column ERROR");
                Mouse.OverrideCursor = Cursors.Wait;
                return false;
            }
            return true;
        }

        private MessageBoxResult CheckError_IfDataExist(int RowCount)
        {//Sprawdzenie czy dane istnieją w systemie => jak istnieją i użytkownik się zgadza to czyści dane (wpisując 0 w quantity)
            if (RowCount != 0)
            {
                Mouse.OverrideCursor = null;
                var Results = MessageBox.Show("Data Exist in DataBase, do you wont replace this data?", "Data Exist", MessageBoxButton.YesNo);
                Mouse.OverrideCursor = Cursors.Wait;
                return Results;
            }
            return MessageBoxResult.Yes;
        }

        private void UpdateDataToDataBase_ANCMonth()
        {
            var context = new DataBaseConnetionContext();

            string[] Data = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if(CheckIfDataIsOK(Data[0]))
            {
                MessageBox.Show("This is NOT ANC Data", "Error!");
                return;
            }

            var DataBaseContent = context.ANC_Monthly.Where(u => u.Year == _year && u.Month == _month).ToList();

            if (!CheckError_QuantityColum(Data[0]))
                return;

            if (CheckError_IfDataExist(DataBaseContent.Count) == MessageBoxResult.Yes)
            {
                foreach (var BaseRecord in DataBaseContent)
                {
                    BaseRecord.Quantity = 0;
                    context.ANC_Monthly.Update(BaseRecord);
                }
                context.SaveChanges();
            }
            else
                return;

            foreach (var DataLine in Data)
            {
                string[] Record = DataLine.Split(' ');

                if (Record[0] != string.Empty && Record.Length == _columns)
                {
                    var BaseRecord = DataBaseContent.Where(u => u.ANC == Record[0]).FirstOrDefault();

                    if (BaseRecord != null)
                    {
                        if (decimal.TryParse(Record[1], out decimal Value))
                        {
                            if (Value != 0)
                            {
                                BaseRecord.Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero);
                                context.ANC_Monthly.Update(BaseRecord);
                                _UpdateRecord++;
                            }
                        }
                    }
                    else
                    {
                        if (decimal.TryParse(Record[1], out decimal Value))
                        {
                            if (Value != 0)
                            {
                                BaseRecord = new MonthlyANC_DB()
                                {
                                    ANC = Record[0],
                                    Year = _year,
                                    Month = _month,
                                    Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero),
                                };
                                context.ANC_Monthly.Add(BaseRecord);
                                _NewRecord++;
                            }
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        private void UpdateDataToDataBase_PNCMonth()
        {
            var context = new DataBaseConnetionContext();

            string[] Data = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (!CheckIfDataIsOK(Data[0]))
            {
                MessageBox.Show("This is NOT PNC Data", "Error!");
                return;
            }

            var DataBaseContent = context.PNC_Monthly.Where(u => u.Year == _year && u.Month == _month).ToList();

            if (!CheckError_QuantityColum(Data[0]))
                return;

            if (CheckError_IfDataExist(DataBaseContent.Count) == MessageBoxResult.Yes)
            {
                foreach (var BaseRecord in DataBaseContent)
                {
                    BaseRecord.Quantity = 0;
                    context.PNC_Monthly.Update(BaseRecord);
                }
                context.SaveChanges();
            }
            else
                return;

            foreach (var DataLine in Data)
            {
                string[] Record = DataLine.Split(' ');

                if (Record[0] != string.Empty && Record.Length == _columns)
                {
                    var BaseRecord = DataBaseContent.Where(u => u.PNC == Record[0]).FirstOrDefault();

                    if (BaseRecord != null)
                    {
                        if (decimal.TryParse(Record[1], out decimal Value))
                        {
                            if (Value != 0)
                            {
                                BaseRecord.Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero);
                                context.PNC_Monthly.Update(BaseRecord);
                                _UpdateRecord++;
                                PNC_Sum(BaseRecord.PNC, BaseRecord.Quantity, BaseRecord.Month);
                            }
                        }
                    }
                    else
                    {
                        if (decimal.TryParse(Record[1], out decimal Value))
                        {
                            if (Value != 0)
                            {
                                BaseRecord = new MonthlyPNC_DB()
                                {
                                    PNC = Record[0],
                                    Year = _year,
                                    Month = _month,
                                    Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero),
                                };
                                context.PNC_Monthly.Add(BaseRecord);
                                _NewRecord++;
                                PNC_Sum(BaseRecord.PNC, BaseRecord.Quantity, BaseRecord.Month);
                            }
                        }
                    }
                }
            }
            context.SaveChanges();
            PNCTotalityRevision_Update();
        }

        private void UpdateDataToDataBase_ANCRevision()
        {
            var context = new DataBaseConnetionContext();

            string[] Data = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (CheckIfDataIsOK(Data[0]))
            {
                MessageBox.Show("This is NOT ANC Data", "Error!");
                return;
            }

            var DataBaseContent = context.ANC_Revision.Where(u => u.Year == _year && u.Revision == _revision).ToList();

            if (!CheckError_QuantityColum(Data[0]))
                return;

            if (CheckError_IfDataExist(DataBaseContent.Count) == MessageBoxResult.Yes)
            {
                foreach (var BaseRecord in DataBaseContent)
                {
                    BaseRecord.Quantity = 0;
                    context.ANC_Revision.Update(BaseRecord);
                }
                context.SaveChanges();
            }
            else
                return;

            foreach (var DataLine in Data)
            {
                string[] Record = DataLine.Split(' ');

                if (Record[0] != string.Empty && Record.Length == _columns)
                {
                    //Tu dodac pentle na każdy miesiąc osobno i to przeorać.
                    for (int column = _revisonStart; column < 13; column++)
                    {
                        var BaseRecord = DataBaseContent.Where(u => u.ANC == Record[0] && u.Month == column).FirstOrDefault();

                        if (BaseRecord != null)
                        {
                            if (decimal.TryParse(Record[column - _revisonStart + 1], out decimal Value))
                            {
                                if (Value != 0)
                                {
                                    BaseRecord.Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero);
                                    context.ANC_Revision.Update(BaseRecord);
                                    _UpdateRecord++;
                                }
                            }
                        }
                        else
                        {
                            if (decimal.TryParse(Record[column - _revisonStart + 1], out decimal Value))
                            {
                                if (Value != 0)
                                {
                                    BaseRecord = new RevisionANC_DB()
                                    {
                                        ANC = Record[0],
                                        Year = _year,
                                        Month = column,
                                        Revision = _revision,
                                        Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero),
                                    };
                                    context.ANC_Revision.Add(BaseRecord);
                                    _NewRecord++;
                                }
                            }
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        private void UpdateDataToDataBase_PNCRevision()
        {
            var context = new DataBaseConnetionContext();

            string[] Data = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (!CheckIfDataIsOK(Data[0]))
            {
                MessageBox.Show("This is NOT PNC Data", "Error!");
                return;
            }

            var DataBaseContent = context.PNC_Revision.Where(u => u.Year == _year && u.Revision == _revision).ToList();

            if (!CheckError_QuantityColum(Data[0]))
                return;

            if (CheckError_IfDataExist(DataBaseContent.Count) == MessageBoxResult.Yes)
            {
                foreach (var BaseRecord in DataBaseContent)
                {
                    BaseRecord.Quantity = 0;
                    context.PNC_Revision.Update(BaseRecord);
                }
                context.SaveChanges();
            }
            else
                return;

            foreach (var DataLine in Data)
            {
                string[] Record = DataLine.Split(' ');

                if (Record[0] != string.Empty && Record.Length == _columns)
                {
                    for (int column = _revisonStart; column < 13; column++)
                    {
                        var BaseRecord = DataBaseContent.Where(u => u.PNC == Record[0] && u.Month == column).FirstOrDefault();

                        if (BaseRecord != null)
                        {
                            if (decimal.TryParse(Record[column- _revisonStart +1], out decimal Value))
                            {
                                if (Value != 0)
                                {
                                    BaseRecord.Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero);
                                    context.PNC_Revision.Update(BaseRecord);
                                    PNC_Sum(BaseRecord.PNC, BaseRecord.Quantity, BaseRecord.Month);
                                    _UpdateRecord++;
                                }
                            }
                        }
                        else
                        {
                            if (decimal.TryParse(Record[column - _revisonStart + 1], out decimal Value))
                            {
                                if (Value != 0)
                                {
                                    BaseRecord = new RevisionPNC_DB()
                                    {
                                        PNC = Record[0],
                                        Year = _year,
                                        Month = column,
                                        Revision = _revision,
                                        Quantity = Math.Round(Value, 4, MidpointRounding.AwayFromZero),
                                    };
                                    context.PNC_Revision.Add(BaseRecord);
                                    PNC_Sum(BaseRecord.PNC, BaseRecord.Quantity, BaseRecord.Month);
                                    _NewRecord++;
                                }
                            }
                        }
                    }
                }
            }
            context.SaveChanges();

            PNCTotalityRevision_Update();
        }

        private void PNCTotalityRevision_Update()
        {
            var context = new DataBaseConnetionContext();
            List<PNCTotality_DB> Records = new List<PNCTotality_DB>();
            int FinishCalc = 12;
            int StartCalc = _revisonStart;

            if(_revision == null)
            {
                Records = context.PNC_Totality.Where(u => u.Year == _year && u.Revision == _revision && u.Month == _month).ToList();
                StartCalc = _month;
                FinishCalc = _month;
            }
            else
            {
                Records = context.PNC_Totality.Where(u => u.Year == _year && u.Revision == _revision).ToList();
            }


            if(Records.Count != 0)
            {
                foreach(var Record in Records)
                {
                    context.PNC_Totality.Remove(Record);
                }
                context.SaveChanges();
            }
            else
            {
                for (int month =_revisonStart; month <=FinishCalc; month++)
                {
                    var DMDFS = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "DMD",
                        Instalation = "FS",
                        Month = month,
                        Quantity = DMD_FS[month - 1],
                    };
                    context.PNC_Totality.Add(DMDFS);
                    var DMDFI = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "DMD",
                        Instalation = "FI",
                        Month = month,
                        Quantity = DMD_FI[month - 1],
                    };
                    context.PNC_Totality.Add(DMDFI);
                    var DMDBI = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "DMD",
                        Instalation = "BI",
                        Month = month,
                        Quantity = DMD_BI[month - 1],
                    };
                    context.PNC_Totality.Add(DMDBI);
                    var DMDFSBU = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "DMD",
                        Instalation = "FSBU",
                        Month = month,
                        Quantity = DMD_FSBU[month - 1],
                    };
                    context.PNC_Totality.Add(DMDFSBU);
                    var D45FS = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "D45",
                        Instalation = "FS",
                        Month = month,
                        Quantity = D45_FS[month - 1],
                    };
                    context.PNC_Totality.Add(D45FS);
                    var D45FI = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "D45",
                        Instalation = "FI",
                        Month = month,
                        Quantity = D45_FI[month - 1],
                    };
                    context.PNC_Totality.Add(D45FI);
                    var D45BI = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "D45",
                        Instalation = "BI",
                        Month = month,
                        Quantity = D45_BI[month - 1],
                    };
                    context.PNC_Totality.Add(D45BI);
                    var D45FSBU = new PNCTotality_DB()
                    {
                        Year = _year,
                        Revision = _revision,
                        Structure = "D45",
                        Instalation = "FSBU",
                        Month = month,
                        Quantity = D45_FSBU[month - 1],
                    };
                    context.PNC_Totality.Add(D45FSBU);
                    if (Proxy[month-1] != 0)
                    {
                        var Proxxy = new PNCTotality_DB()
                        {
                            Year = _year,
                            Revision = _revision,
                            Structure = "Proxy",
                            Instalation = null,
                            Month = month,
                            Quantity = D45_FSBU[month - 1],
                        };
                        context.PNC_Totality.Add(Proxxy);
                    }
                }
                context.SaveChanges();
            }
        }

        private void PNC_Sum(string PNC, decimal Quantity, int Month)
        {
            if (PNC.Remove(3) == "911")
            {
                if (PNC.Remove(4, 5).Remove(0, 3) == "5")
                {
                    if (PNC.Remove(5, 4).Remove(0, 4) == "1")
                        DMD_FS[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "2")
                        DMD_BI[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "3")
                        DMD_FI[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "4")
                        DMD_FSBU[Month - 1] += Quantity;
                }
                else if (PNC.Remove(4, 5).Remove(0, 3) == "0")
                {
                    if (PNC.Remove(5, 4).Remove(0, 4) == "5")
                        D45_FS[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "6")
                        D45_BI[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "7")
                        D45_FI[Month - 1] += Quantity;
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "8")
                        D45_FSBU[Month - 1] += Quantity;
                }
            }
            else
            {
                Proxy[Month - 1] += Quantity;
            }
        }

        private bool CheckIfDataIsOK(string Record)
        {
            if (Record.Remove(3) == "911")
                return true;

            return false;

        }
    }
}
