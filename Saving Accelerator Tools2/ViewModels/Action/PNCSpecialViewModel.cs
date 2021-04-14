using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class PNCSpecialViewModel : INotifyProperty
    {
        #region Constructors
        public PNCSpecialViewModel()
        {
            Mediator.Mediator.Register("PNCSpecial_LoadNewData", LoadNewData);
            Mediator.Mediator.Register("Set_Year_ForPNCSPecial", Set_Year_ForPNCSPecial);
            Mediator.Mediator.Register("Set_Visibility_PNCSpecial", Set_Visibility);
            Mediator.Mediator.Register("PNCSpecial_Load", LoadData);
            Mediator.Mediator.Register("PNCSpecial_Save", SaveData);
            Mediator.Mediator.Register("PNCSpecial_Save_Object", SaveData2);
        }
        ~PNCSpecialViewModel()
        {
            Mediator.Mediator.Unregister("PNCSpecial_LoadNewData", LoadNewData);
            Mediator.Mediator.Unregister("Set_Year_ForPNCSPecial", Set_Year_ForPNCSPecial);
            Mediator.Mediator.Unregister("Set_Visibility_PNCSpecial", Set_Visibility);
            Mediator.Mediator.Unregister("PNCSpecial_Load", LoadData);
            Mediator.Mediator.Unregister("PNCSpecial_Save", SaveData);
            Mediator.Mediator.Unregister("PNCSpecial_Save_Object", SaveData2);
        }
        #endregion

        #region Private Variables
        private Visibility _Contorl_Visibility = Visibility.Hidden;
        private bool _Collapsed = false;
        private bool _Expanded = true;
        private List<PNCSpecialTable> _TableData = new List<PNCSpecialTable>();
        private List<PNCSpecialModel> _DataModel;
        private int _Year = 0;
        #endregion

        #region Public Variables
        public Visibility ControlVisibility
        {
            get { return _Contorl_Visibility; }
            set
            {
                _Contorl_Visibility = value;
                if (value == Visibility.Hidden)
                {
                    TableData = new List<PNCSpecialTable>();
                    if (_DataModel != null)
                        _DataModel.Clear();
                }
                RisePropoertyChanged();
            }
        }
        public bool Collapsed
        {
            get { return _Collapsed; }
            set
            {
                _Collapsed = value;
                if (value == true)
                {
                    Expanded = false;
                }
                else if (_Expanded == false)
                {
                    Expanded = true;
                }
                PrepareDataforDataGrid();
                RisePropoertyChanged();
            }
        }
        public bool Expanded
        {
            get { return _Expanded; }
            set
            {
                _Expanded = value;
                if (value == true)
                {
                    Collapsed = false;
                }
                else if (_Collapsed == false)
                {
                    Collapsed = true;
                }
                PrepareDataforDataGrid();
                RisePropoertyChanged();
            }
        }
        public List<PNCSpecialTable> TableData
        {
            get { return _TableData; }
            set
            {
                _TableData = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Function
        private void PrepareDataforDataGrid()
        {
            var NewTableData = new List<PNCSpecialTable>();

            foreach (var DataModelRecord in _DataModel)
            {
                var NewPNCRecord = new PNCSpecialTable()
                {
                    PNC = DataModelRecord.PNC,
                    ECCC = DataModelRecord.ECCC,
                    Old_STK = DataModelRecord.Old_STK,
                    New_STK = DataModelRecord.New_STK,
                    Delta = DataModelRecord.Delta,
                };
                NewTableData.Add(NewPNCRecord);
                if (_Expanded)
                {
                    foreach (var DataModelRecordANC in DataModelRecord.ANCChange)
                    {
                        var NewANCRecord = new PNCSpecialTable()
                        {
                            Old_ANC = DataModelRecordANC.Old_ANC,
                            Old_Q = DataModelRecordANC.Old_Q,
                            New_ANC = DataModelRecordANC.New_ANC,
                            New_Q = DataModelRecordANC.New_Q,
                            Old_STK = DataModelRecordANC.Old_STK,
                            New_STK = DataModelRecordANC.New_STK,
                            Delta = DataModelRecordANC.Delta,
                        };
                        NewTableData.Add(NewANCRecord);
                    }
                }
            }
            TableData = NewTableData;
        }
        private void LoadSTK()
        {
            List<STK_DB> LoadedSTK = new List<STK_DB>();
            string MissingSTK = string.Empty;
            if (_Year == 0)
            {
                do
                {
                    Mediator.Mediator.NotifyColleagues("Get_Year_ForPNCSPecial", null);
                    Thread.Sleep(50);
                }
                while (_Year == 0);
            }

            foreach (var PNCRecord in _DataModel)
            {
                foreach (var ANCRecord in PNCRecord.ANCChange)
                {
                    //ładowanie STK dla OLD ANC
                    if (ANCRecord.Old_ANC != string.Empty)
                    {
                        if (LoadedSTK.Any(c => c.ANC == ANCRecord.Old_ANC))
                        {
                            ANCRecord.Old_STK = Math.Round(LoadedSTK.Where(c => c.ANC == ANCRecord.Old_ANC).First().STD * ANCRecord.Old_Q, 4, MidpointRounding.AwayFromZero);
                            PNCRecord.Old_STK += ANCRecord.Old_STK;
                        }
                        else
                        {
                            var LoadSTK = STK_Controller.FindItem(ANCRecord.Old_ANC, _Year);
                            if (LoadSTK != null)
                            {
                                LoadedSTK.Add(LoadSTK);
                                ANCRecord.Old_STK = Math.Round(LoadSTK.STD * ANCRecord.Old_Q, 4, MidpointRounding.AwayFromZero);
                                PNCRecord.Old_STK += ANCRecord.Old_STK;
                            }
                            else
                            {
                                MissingSTK += ANCRecord.Old_ANC + Environment.NewLine;
                            }
                        }
                    }
                    //Ładowanie STK dla NEW ANC
                    if (ANCRecord.New_ANC != string.Empty)
                    {
                        if (LoadedSTK.Any(c => c.ANC == ANCRecord.New_ANC))
                        {
                            ANCRecord.New_STK = Math.Round(LoadedSTK.Where(c => c.ANC == ANCRecord.New_ANC).First().STD * ANCRecord.New_Q, 4, MidpointRounding.AwayFromZero);
                            PNCRecord.New_STK += ANCRecord.New_STK;
                        }
                        else
                        {
                            var LoadSTK = STK_Controller.FindItem(ANCRecord.New_ANC, _Year);
                            if (LoadSTK != null)
                            {
                                LoadedSTK.Add(LoadSTK);
                                ANCRecord.New_STK = Math.Round(LoadSTK.STD * ANCRecord.New_Q, 4, MidpointRounding.AwayFromZero);
                                PNCRecord.New_STK += ANCRecord.New_STK;
                            }
                            else
                            {
                                if (!MissingSTK.Contains(ANCRecord.New_ANC))
                                {
                                    MissingSTK += ANCRecord.New_ANC + Environment.NewLine;
                                }
                            }
                        }
                    }
                    //Liczymy Delte
                    ANCRecord.Delta = Math.Round(ANCRecord.Old_STK - ANCRecord.New_STK, 4, MidpointRounding.AwayFromZero);
                    PNCRecord.Delta += ANCRecord.Delta;
                }
            }
            if (MissingSTK != string.Empty)
            {
                MessageBox.Show("List ANC with missing STK in DataBase" + Environment.NewLine + MissingSTK + "Please check if STK is present in FSDS and after that contact with Administrator", "Warninrg!", MessageBoxButton.OK);
            }
        }
        #endregion

        #region Mediatord Functions
        private void LoadNewData(object _NewData)
        {
            List<PNCSpecialModel> NewData = new List<PNCSpecialModel>(_NewData as List<PNCSpecialModel>);
            int NewRecord = 0;

            Mouse.OverrideCursor = Cursors.Wait;

            if (_DataModel != null)
            {
                var ResultReplaceData = MessageBox.Show("Do you want replace data?", "Warning!", MessageBoxButton.YesNo);
                if (ResultReplaceData == MessageBoxResult.Yes)
                {
                    _DataModel = NewData;
                    NewRecord = NewData.Count();
                }
                else if (ResultReplaceData == MessageBoxResult.No)
                {
                    var ResultAddData = MessageBox.Show("Do you want add new data to exist data?", "Warning!", MessageBoxButton.YesNo);
                    if (ResultAddData == MessageBoxResult.Yes)
                    {
                        foreach (var NewDataRecord in NewData)
                        {
                            if (!_DataModel.Any(c => c.PNC == NewDataRecord.PNC))
                            {
                                //NewRecord
                                NewRecord++;
                                _DataModel.Add(NewDataRecord);
                            }
                        }
                    }
                }
            }
            else
            {
                _DataModel = NewData;
                NewRecord = NewData.Count();
            }
            LoadSTK();
            PrepareDataforDataGrid();
            _ = new Calculation_ActionView();
            MessageBox.Show("Add " + NewRecord.ToString() + " Records", "Notification", MessageBoxButton.OK);

            Mouse.OverrideCursor = null;
        }
        private void Set_Year_ForPNCSPecial(object Year)
        {
            _Year = Convert.ToInt32((decimal)Year);
        }
        private void Set_Visibility(object NewVisibility)
        {
            ControlVisibility = (Visibility)NewVisibility;
        }
        private void LoadData(object LoadedData)
        {
            _DataModel = LoadedData as List<PNCSpecialModel>;
            PrepareDataforDataGrid();
        }
        private void SaveData(object obj)
        {
            Mediator.Mediator.NotifyColleagues("PNCSpecial_Items", _DataModel);
        }
        private void SaveData2(object Data)
        {
            foreach (var DataItem in _DataModel)
            {
                (Data as List<PNCSpecialModel>).Add(DataItem);
            }
        }

        #endregion
    }

    public class PNCSpecialTable
    {
        public string PNC { get; set; }
        public decimal ECCC { get; set; }
        public string Old_ANC { get; set; }
        public decimal Old_Q { get; set; }
        public string New_ANC { get; set; }
        public decimal New_Q { get; set; }
        public decimal Old_STK { get; set; }
        public decimal New_STK { get; set; }
        public decimal Delta { get; set; }
    }
}
