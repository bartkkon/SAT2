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
        private bool ErrorConditions = false;
        private bool STKAdd = false;
        private List<MonthlyANC_DB> ANCMonthly;
        private List<MonthlyPNC_DB> PNCMonthly;
        private List<RevisionANC_DB> ANCRevision;
        private List<RevisionPNC_DB> PNCRevision;
        private List<PNCTotality_DB> PNCTotal;


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

        public AddingData(string Configuration, decimal Year, int Month)
        {
            InitializeComponent();
            _year = Year;
            _month = Month;
            _configuration = Configuration;
            _revisonStart = 0;
            _revision = "EA4";

            if (Configuration == "ANC")
            {
                _columns = 2;
                this.Title = "Adding Monthly Data => ANC";
                InstructionBlock_Text.Text =
                    "--------------------------" + Environment.NewLine +
                    "ANC    <space>    Quantity" + Environment.NewLine +
                    "--------------------------";
            }
            else if (Configuration == "PNC")
            {
                _columns = 2;
                this.Title = "Adding Monthly Data => PNC";
                InstructionBlock_Text.Text =
                    "--------------------------" + Environment.NewLine +
                    "PNC    <space>    Quantity" + Environment.NewLine +
                    "--------------------------";
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
                _columns = 8;
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
            if (!STKAdd)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                AddDataToDataBase();
                Mouse.OverrideCursor = null;

                if (!ErrorConditions)
                    if (_DeletedRecord != 0 || _NewRecord != 0)
                        MessageBox.Show("Completed:" + Environment.NewLine +
                            "Deleteded Record:    " + _DeletedRecord.ToString() + Environment.NewLine +
                            "New Record:          " + _NewRecord.ToString(),
                            "Updated completed!");

                this.Close();
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
                        NewBaseRecord.STD = Math.Round(STDRecults,4, MidpointRounding.AwayFromZero);
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

        private void AddDataToDataBase()
        {
            string[] DataToAdd = New_Data_TextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            //Sprawdzenie czy przypadkiem nie pomyliliśmy się i nie próbujemy dodać PNC do ANC i na odwrót
            if (_configuration == "PNC")
            {
                if (!CheckIfDataIsOK(DataToAdd[0]))
                {
                    MessageBox.Show("This is NOT PNC Data", "Error!");
                    ErrorConditions = true;
                    return;
                }
            }
            else if (_configuration == "ANC")
            {
                if (CheckIfDataIsOK(DataToAdd[0]))
                {
                    MessageBox.Show("This is NOT ANC Data", "Error!");
                    ErrorConditions = true;
                    return;
                }
            }
            else
            {
                return;
            }

            if (CheckError_QuantityColum(DataToAdd[0]))
            {
                ErrorConditions = true;
                return;
            }

            if (_revision == "EA4")
            {
                if (_configuration == "ANC")
                {
                    ANCMonthly = ANCMonthly_Controller.Load(_year, _month).ToList();
                    if (CheckError_IfDataExist(ANCMonthly.Count()) == MessageBoxResult.Yes)
                    {
                        ANCMonthly_Controller.Delete(ANCMonthly);
                        _DeletedRecord = ANCMonthly.Count();
                        ANCMonthly = new List<MonthlyANC_DB>();
                    }
                    else
                    {
                        return;
                    }

                    foreach (var Row in DataToAdd)
                    {
                        if (Row.Length < 9)
                            continue;

                        string[] SeparateRow = Row.Split(' ');

                        if (decimal.TryParse(SeparateRow[1], out decimal result))
                        {
                            if (result != 0)
                            {
                                var NewRecord = new MonthlyANC_DB()
                                {
                                    ANC = SeparateRow[0],
                                    Year = _year,
                                    Month = _month,
                                    Quantity = Math.Round(result, 4, MidpointRounding.AwayFromZero),
                                };
                                ANCMonthly.Add(NewRecord);
                            }
                        }
                    }
                    ANCMonthly_Controller.Add(ANCMonthly);
                    _NewRecord = ANCMonthly.Count();
                }
                else if (_configuration == "PNC")
                {
                    PNCMonthly = PNCMonthly_Controller.Load(_year, _month).ToList();
                    PNCTotal = PNCTotally_Controler.Load(_year, "EA4", _month).ToList();
                    if (CheckError_IfDataExist(PNCMonthly.Count()) == MessageBoxResult.Yes)
                    {
                        PNCMonthly_Controller.Delete(PNCMonthly);
                        _DeletedRecord = PNCMonthly.Count();
                        PNCTotally_Controler.Delete(PNCTotal);
                        PNCMonthly = new List<MonthlyPNC_DB>();
                    }
                    else
                    {
                        return;
                    }

                    PNCTotal_NewObject(_month, _month);

                    foreach (var Row in DataToAdd)
                    {
                        if (Row.Length < 9)
                            continue;

                        string[] SeparateRow = Row.Split(' ');

                        if (decimal.TryParse(SeparateRow[1], out decimal result))
                        {
                            if (result != 0)
                            {
                                var NewRecord = new MonthlyPNC_DB()
                                {
                                    PNC = SeparateRow[0],
                                    Year = _year,
                                    Month = _month,
                                    Quantity = Math.Round(result, 4, MidpointRounding.AwayFromZero),
                                };
                                PNCMonthly.Add(NewRecord);
                                PNC_Sum(NewRecord.PNC, NewRecord.Quantity, NewRecord.Month);
                            }
                        }
                    }
                    PNCMonthly_Controller.Add(PNCMonthly);
                    PNCTotally_Controler.Add(PNCTotal);
                    _NewRecord = PNCMonthly.Count();
                }
            }
            else
            {
                if (_configuration == "ANC")
                {
                    ANCRevision = RevisionANC_Controller.Load(_year, _revision).ToList();
                    if (CheckError_IfDataExist(ANCRevision.Count()) == MessageBoxResult.Yes)
                    {
                        RevisionANC_Controller.Delete(ANCRevision);
                        _DeletedRecord = ANCRevision.Count();
                        ANCRevision = new List<RevisionANC_DB>();
                    }
                    else
                    {
                        return;
                    }

                    foreach (var Row in DataToAdd)
                    {
                        if (Row.Length < 9)
                            continue;

                        string[] SeparateRow = Row.Split(' ');
                        for (int counter = 1; counter <= (13 - _revisonStart); counter++)
                        {
                            if (decimal.TryParse(SeparateRow[counter], out decimal result))
                            {
                                if (result != 0)
                                {
                                    var NewRecord = new RevisionANC_DB()
                                    {
                                        ANC = SeparateRow[0],
                                        Year = _year,
                                        Month = counter + _revisonStart - 1,
                                        Revision = _revision,
                                        Quantity = Math.Round(result, 4, MidpointRounding.AwayFromZero),
                                    };
                                    ANCRevision.Add(NewRecord);
                                }
                            }
                        }
                    }
                    RevisionANC_Controller.Add(ANCRevision);
                    _NewRecord = ANCRevision.Count();
                }
                else if (_configuration == "PNC")
                {
                    PNCRevision = RevisonPNC_Controller.Load(_year, _revision).ToList();
                    PNCTotal = PNCTotally_Controler.Load(_year, _revision, 0).ToList();
                    if (CheckError_IfDataExist(PNCRevision.Count()) == MessageBoxResult.Yes)
                    {
                        RevisonPNC_Controller.Delete(PNCRevision);
                        PNCTotally_Controler.Delete(PNCTotal);
                        _DeletedRecord = PNCRevision.Count();
                        PNCRevision = new List<RevisionPNC_DB>();
                    }
                    else
                    {
                        return;
                    }

                    PNCTotal_NewObject(_revisonStart, 12);

                    foreach (var Row in DataToAdd)
                    {
                        if (Row.Length < 9)
                            continue;

                        string[] SeparateRow = Row.Split(' ');
                        for (int counter = 1; counter <= (13 - _revisonStart); counter++)
                        {
                            if (decimal.TryParse(SeparateRow[counter], out decimal result))
                            {
                                if (result != 0)
                                {
                                    var NewRecord = new RevisionPNC_DB()
                                    {
                                        PNC = SeparateRow[0],
                                        Year = _year,
                                        Month = counter + _revisonStart - 1,
                                        Revision = _revision,
                                        Quantity = Math.Round(result, 4, MidpointRounding.AwayFromZero),
                                    };
                                    PNCRevision.Add(NewRecord);
                                    PNC_Sum(NewRecord.PNC, NewRecord.Quantity, NewRecord.Month);
                                }
                            }
                        }
                    }
                    RevisonPNC_Controller.Add(PNCRevision);
                    PNCTotally_Controler.Add(PNCTotal);
                    _NewRecord = PNCRevision.Count();
                }
            }
        }

        private bool CheckError_QuantityColum(string Row)
        {//Sprawdzenie czy wprowadzone dane są w odpowiedniej ilości kolumn
            if (Row.Split(' ').Length != _columns)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Wrong numer of columns!!!", "Column ERROR");
                Mouse.OverrideCursor = Cursors.Wait;
                return true;
            }
            return false;
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

        private void PNCTotal_NewObject(int startMonth, int finishMonth)
        {
            PNCTotal = new List<PNCTotality_DB>();

            for (int month = startMonth; month <= finishMonth; month++)
            {
                var DMDFS = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "DMD",
                    Instalation = "FS",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(DMDFS);
                var DMDFI = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "DMD",
                    Instalation = "FI",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(DMDFI);
                var DMDBI = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "DMD",
                    Instalation = "BI",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(DMDBI);
                var DMDFSBU = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "DMD",
                    Instalation = "FSBU",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(DMDFSBU);
                var D45FS = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "D45",
                    Instalation = "FS",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(D45FS);
                var D45FI = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "D45",
                    Instalation = "FI",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(D45FI);
                var D45BI = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "D45",
                    Instalation = "BI",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(D45BI);
                var D45FSBU = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "D45",
                    Instalation = "FSBU",
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(D45FSBU);

                var Proxxy = new PNCTotality_DB()
                {
                    Year = _year,
                    Revision = _revision,
                    Structure = "Proxy",
                    Instalation = null,
                    Month = month,
                    Quantity = 0,
                };
                PNCTotal.Add(Proxxy);

            }


        }

        private void PNC_Sum(string PNC, decimal Quantity, int Month)
        {

            if (PNC.Remove(3) == "911")
            {
                if (PNC.Remove(4, 5).Remove(0, 3) == "5")
                {
                    if (PNC.Remove(5, 4).Remove(0, 4) == "1")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "DMD" && c.Instalation == "FS" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "2")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "DMD" && c.Instalation == "BI" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "3")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "DMD" && c.Instalation == "FI" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "4")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "DMD" && c.Instalation == "FSBU" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                }
                else if (PNC.Remove(4, 5).Remove(0, 3) == "0")
                {
                    if (PNC.Remove(5, 4).Remove(0, 4) == "5")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "D45" && c.Instalation == "FS" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "6")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "D45" && c.Instalation == "BI" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "7")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "D45" && c.Instalation == "FI" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                    else if (PNC.Remove(5, 4).Remove(0, 4) == "8")
                    {
                        var Find = PNCTotal.Where(c => c.Structure == "D45" && c.Instalation == "FSBU" && c.Month == Month).First();
                        Find.Quantity += Quantity;
                    }
                }
            }
            else
            {
                {
                    var Find = PNCTotal.Where(c => c.Structure == "Proxy" && c.Month == Month).First();
                    Find.Quantity += Quantity;
                }
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
