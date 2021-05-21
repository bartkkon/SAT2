using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;
using Saving_Accelerator_Tools2.Mediator;
using System.Windows.Input;
using System.Windows;
using Saving_Accelerator_Tools2.ViewModels.Action;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class LoadAction
    {
        private int _ActionID;
        private Action_DB ActionDataBase = new Action_DB();
        private General_Information_Model GeneralInformation = new General_Information_Model();
        private PlatformModel Platform = new PlatformModel();
        private List<ANCChangeModel> ANCChange = new List<ANCChangeModel>();
        private ECCCModel ECCCValue = new ECCCModel();
        private List<PNCListData> PNCList = new List<PNCListData>();
        private List<PlusMinusModel> ANCSpecialPlusMinus = new List<PlusMinusModel>();
        private List<ANCSpecialModels> ANCSpecialPlatform = new List<ANCSpecialModels>();
        private List<PNCSpecialModel> PNCSpecial = new List<PNCSpecialModel>();
        private List<CalculationModels> ResultsModel = new List<CalculationModels>();

        public LoadAction(int ActionID)
        {
            if(ActionID == 0)
            {
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;
            Permission.Permission.Check.ReCalculation_Open = false;

            _ActionID = ActionID;
            if (LoadModels())
            {
                Prepare();
            }

            Permission.Permission.Check.ReCalculation_Open = true;
            Mouse.OverrideCursor = null;
        }

        public LoadAction(bool NewAction)
        {
            if(!NewAction)
            {
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;
            Permission.Permission.Check.ReCalculation_Open = false;

            _ActionID = 0;
            DataForNewAction();
            Prepare();

            Permission.Permission.Check.ReCalculation_Open = true;
            Mouse.OverrideCursor = null;
        }

        private bool LoadModels()
        {
            ActionDataBase = Action_Controller.Load_Active(_ActionID);

            if (ActionDataBase.ActionID != _ActionID)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Please select actions again.", "Something go wrong!");
                return false;
            }

            return true;
        }

        private void DataForNewAction()
        {
            ActionDataBase.StartYear = DateTime.UtcNow.Year;
            ActionDataBase.Month = DateTime.UtcNow.Month;
            ActionDataBase.QEstymation = 100;
            ActionDataBase.Active = true;
            ActionDataBase.Calculation = 1;
        }

        private void Prepare()
        {
            Prepare_General_Information();
            Prepare_Platform();
            Preapre_ANCChange();
            Prepare_ECCC();
            switch (ActionDataBase.Calculation)
            {
                case 2:
                    Prepare_PlusMinus_ANCSpecial();
                    Prepare_Platform_ANCSpecial();
                    break;
                case 3:
                    Prepare_PNC();
                    break;
                case 4:
                    Prepare_PNCSpecial();
                    break;
                default:
                    break;
            }
            Prepare_Results();
        }

        private void Prepare_General_Information()
        {
            GeneralInformation.ID = ActionDataBase.ID;
            GeneralInformation.ActionID = ActionDataBase.ActionID;
            GeneralInformation.Name = ActionDataBase.Name;
            GeneralInformation.Description = ActionDataBase.Description;
            GeneralInformation.Active = ActionDataBase.Active;
            GeneralInformation.StartYear = ActionDataBase.StartYear;
            GeneralInformation.Month = ActionDataBase.Month;
            GeneralInformation.Devision = ActionDataBase.Action_Devision.Count == 0 ? null : ActionDataBase.Action_Devision[0].Devision;
            GeneralInformation.Plant = ActionDataBase.Action_Plant.Count == 0 ? null : ActionDataBase.Action_Plant[0].Plant;
            GeneralInformation.Leader =  ActionDataBase.Action_Leader.Count == 0 ? null : ActionDataBase.Action_Leader[0].Leader;
            GeneralInformation.Tag =  ActionDataBase.Action_Tag.Count == 0 ? null : ActionDataBase.Action_Tag[0].Tag;

            Mediator.Mediator.NotifyColleagues("General_Information_Load", GeneralInformation);
            Mediator.Mediator.NotifyColleagues("Set_QuantityEstymation", ActionDataBase.QEstymation);
            Mediator.Mediator.NotifyColleagues("Set_Calc", ActionDataBase.Calculation);
        }
        private void Prepare_Platform()
        {
            Platform.DMD = ActionDataBase.DMD;
            Platform.D45 = ActionDataBase.D45;
            Platform.FS = ActionDataBase.FS;
            Platform.FI = ActionDataBase.FI;
            Platform.BI = ActionDataBase.BI;
            Platform.FSBU = ActionDataBase.FSBU;

            Mediator.Mediator.NotifyColleagues("SetPlatform", Platform);
        }
        private void Preapre_ANCChange()
        {
            foreach (var ANCRow in ActionDataBase.Action_ANCChange)
            {
                var NewRow = new ANCChangeModel()
                {
                    Year = ActionDataBase.StartYear,
                    ID = ANCRow.ANCChange.ID,
                    OldANC = ANCRow.ANCChange.OldANC,
                    NewANC = ANCRow.ANCChange.NewANC,
                    OldANCQuantity = ANCRow.ANCChange.OldANCQuantity,
                    NewANCQuantity = ANCRow.ANCChange.NewANCQuantity,
                    OldSTK = ANCRow.ANCChange.OldSTK,
                    NewSTK = ANCRow.ANCChange.NewSTK,
                    Delta = ANCRow.ANCChange.Delta,
                    UserEstymacja = ANCRow.ANCChange.UserEstymacja,
                    Percent = ANCRow.ANCChange.Percent,
                    Estymacja = ANCRow.ANCChange.Estymacja,
                    NextANC1 = ANCRow.ANCChange.NextANC1,
                    NextANC2 = ANCRow.ANCChange.NextANC2,
                };
                ANCChange.Add(NewRow);
            }
            if (ANCChange.Count == 0)
            {
                ANCChange.Add(new ANCChangeModel());
            }
            Mediator.Mediator.NotifyColleagues("Set_ANCChange", ANCChange);
        }
        private void Prepare_ECCC()
        {
            ECCCValue.ECCC = ActionDataBase.ECCC;
            ECCCValue.ECCCSpecial = ActionDataBase.ECCCSpec;
            ECCCValue.ECCC_Value = ActionDataBase.ECCCValue;

            Mediator.Mediator.NotifyColleagues("Set_ECCC", ECCCValue);
        }
        private void Prepare_PlusMinus_ANCSpecial()
        {
            foreach (var Record in ActionDataBase.Action_ANCChange_Items)
            {
                var NewItem = new PlusMinusModel()
                {
                    ID = Record.Item.ID,
                    Item = Record.Item.Item,
                    Plus = Record.Item.Plus,
                    Minus = Record.Item.Minus,
                };
                ANCSpecialPlusMinus.Add(NewItem);
            }
            Mediator.Mediator.NotifyColleagues("Set_ANCSpecial_PLusMinus", ANCSpecialPlusMinus);
        }
        private void Prepare_Platform_ANCSpecial()
        {
            foreach (var record in ActionDataBase.Action_ANCChange_Platform)
            {
                var NewItem = new ANCSpecialModels()
                {
                    ID = record.Platform.ID,
                    Platform = record.Platform.Platform,
                    Installation = record.Platform.Installation,
                };
                ANCSpecialPlatform.Add(NewItem);
            }
            Mediator.Mediator.NotifyColleagues("Set_ANCSpecial_Platform", ANCSpecialPlatform);
        }
        private void Prepare_PNCSpecial()
        {
            foreach (var PNCSpecial_Record in ActionDataBase.Action_PNCSpecial)
            {
                var PNCNewRecord = new PNCSpecialModel()
                {
                    ID = PNCSpecial_Record.PNCSpecID,
                    PNC = PNCSpecial_Record.PNCSpecial.PNC,
                    ECCC = PNCSpecial_Record.PNCSpecial.ECCC,
                    Old_STK = PNCSpecial_Record.PNCSpecial.Old_STK,
                    New_STK = PNCSpecial_Record.PNCSpecial.New_STK,
                    Delta = PNCSpecial_Record.PNCSpecial.Delta,
                };
                foreach (var ANCSpecial_Record in PNCSpecial_Record.PNCSpecial.PNC_ANC_Special)
                {
                    var ANCNewRecord = new PNCSPecial_ANCChangeModel()
                    {
                        ID = ANCSpecial_Record.ANC_ID,
                        Old_ANC = ANCSpecial_Record.ANCChange.Old_ANC,
                        Old_Q = ANCSpecial_Record.ANCChange.Old_Q,
                        Old_STK = ANCSpecial_Record.ANCChange.Old_STK,
                        New_ANC = ANCSpecial_Record.ANCChange.New_ANC,
                        New_Q = ANCSpecial_Record.ANCChange.New_Q,
                        New_STK = ANCSpecial_Record.ANCChange.New_STK,
                        Delta = ANCSpecial_Record.ANCChange.Delta,
                    };
                    PNCNewRecord.ANCChange.Add(ANCNewRecord);
                }
                PNCSpecial.Add(PNCNewRecord);
            }
            Mediator.Mediator.NotifyColleagues("PNCSpecial_Load", PNCSpecial);
        }
        private void Prepare_PNC()
        {
            if (ActionDataBase.Action_PNC.Count != 0)
            {
                foreach (var Record in ActionDataBase.Action_PNC)
                {
                    var NewRecord = new PNCListData()
                    {
                        ID = Record.PNCID,
                        PNC = Record.List.PNC,
                    };
                    PNCList.Add(NewRecord);
                }
                Mediator.Mediator.NotifyColleagues("Set_PNC_Data", PNCList);
            }
        }
        private void Prepare_Results()
        {
            if(ActionDataBase.Action_Results.Count !=0)
            {
                foreach(var Record in ActionDataBase.Action_Results)
                {
                    var PrepareRecord = new CalculationModels()
                    {
                        ID = Record.ResultID,
                        Item = Record.Result.Item,
                        Revision = Record.Result.Revision,
                        Month = Record.Result.Month,
                        CarryOver = Record.Result.CarryOver,
                        Quantity = Record.Result.Quantity,
                        Savings = Record.Result.Savings,
                        ECCC = Record.Result.ECCC,
                        Update = false,
                        ToRemove = false,
                    };
                    ResultsModel.Add(PrepareRecord);
                }
                Mediator.Mediator.NotifyColleagues("Tabels_LoadData", ResultsModel);
            }
        }
    }
}
