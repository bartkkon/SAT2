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
        private Action_DB ActionDataBase;
        private General_Information_Model GeneralInformation = new General_Information_Model();
        private PlatformModel Platform = new PlatformModel();
        private List<ANCChangeModel> ANCChange = new List<ANCChangeModel>();
        private ECCCModel ECCCValue = new ECCCModel();
        private List<PNCListData> PNCList = new List<PNCListData>();
        private List<PlusMinusModel> ANCSpecialPlusMinus = new List<PlusMinusModel>();
        private List<ANCSpecialModels> ANCSpecialPlatform = new List<ANCSpecialModels>();
        private List<PNCSpecialModel> PNCSpecial = new List<PNCSpecialModel>();

        public LoadAction(int ActionID)
        {
            _ActionID = ActionID;
            LoadModels();
        }

        private void LoadModels()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            ActionDataBase = Action_Controller.Load(_ActionID);

            if (ActionDataBase == null)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Please select actions again.", "Something go wrong!");
                return;
            }
            #region Prepare Models to sent
            PrepareGeneral_Information();
            Prepare_Platform();
            Preapre_ANCChange();
            Prepare_ECCC();
            Prepare_Calculation();
            Prepare_PlusMinus_ANCSpecial();
            Prepare_Platform_ANCSpecial();
            Prepare_PNCSpecial();

            #endregion

            #region Sent information to mediator    
            Mediator.Mediator.NotifyColleagues("General_Information_Load", GeneralInformation);
            Mediator.Mediator.NotifyColleagues("SetPlatform", Platform);
            Mediator.Mediator.NotifyColleagues("Set_ANCChange", ANCChange);
            Mediator.Mediator.NotifyColleagues("Set_ECCC", ECCCValue);
            Mediator.Mediator.NotifyColleagues("Set_QuantityEstymation", ActionDataBase.QEstymation);
            Mediator.Mediator.NotifyColleagues("Set_Calc", ActionDataBase.Calculation);
            Mediator.Mediator.NotifyColleagues("Set_PNC_Data", PNCList);
            Mediator.Mediator.NotifyColleagues("Set_ANCSpecial_PLusMinus", ANCSpecialPlusMinus);
            Mediator.Mediator.NotifyColleagues("Set_ANCSpecial_Platform", ANCSpecialPlatform);
            Mediator.Mediator.NotifyColleagues("PNCSpecial_Load", PNCSpecial);

            #endregion
            Mouse.OverrideCursor = null;
        }

        private void Prepare_Calculation()
        {
            if (ActionDataBase.Calculation == 3)
            {
                foreach (var PNCRow in ActionDataBase.Action_PNC)
                {
                    var NewRecord = new PNCListData()
                    {
                        ID = PNCRow.PNCID,
                        PNC = PNCRow.List.PNC,
                    };
                    PNCList.Add(NewRecord);
                }
            }
        }
        private void PrepareGeneral_Information()
        {
            GeneralInformation.ID = ActionDataBase.ID;
            GeneralInformation.ActionID = ActionDataBase.ActionID;
            GeneralInformation.Name = ActionDataBase.Name;
            GeneralInformation.Description = ActionDataBase.Description;
            GeneralInformation.Active = ActionDataBase.Active;
            GeneralInformation.StartYear = ActionDataBase.StartYear;
            GeneralInformation.Month = ActionDataBase.Month;
            GeneralInformation.Devision = ActionDataBase.Action_Devision[0].Devision;
            GeneralInformation.Plant = ActionDataBase.Action_Plant[0].Plant;
            GeneralInformation.Leader = ActionDataBase.Action_Leader[0].Leader;
            GeneralInformation.Tag = ActionDataBase.Action_Tag[0].Tag;
        }
        private void Prepare_Platform()
        {
            Platform.DMD = ActionDataBase.DMD;
            Platform.D45 = ActionDataBase.D45;
            Platform.FS = ActionDataBase.FS;
            Platform.FI = ActionDataBase.FI;
            Platform.BI = ActionDataBase.BI;
            Platform.FSBU = ActionDataBase.FSBU;
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
        }
        private void Prepare_ECCC()
        {
            ECCCValue.ECCC = ActionDataBase.ECCC;
            ECCCValue.ECCCSpecial = ActionDataBase.ECCCSpec;
            ECCCValue.ECCC_Value = ActionDataBase.ECCCValue;
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
        }
    }
}
