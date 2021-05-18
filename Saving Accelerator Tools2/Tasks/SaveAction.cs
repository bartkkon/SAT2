using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks.Calculation_Transfer_Class;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class SaveAction
    {
        #region Private Variables
        //Upadate
        private bool AnyUpdated = false;

        //Model Akcji do zapisu
        private Action_DB UpdatedAction;
        private Action_DB OryginalAction;

        //USerControls Respond
        private General_Information_Model _GeneralInformation = null;
        private PlatformModel _Platform = null;
        private List<ANCChangeModel> _ANCList = new List<ANCChangeModel>();
        //private DataBaseConnetionContext context;
        private ECCCModel _ECCC = null;
        private Estimation_Percent_Transfer _QEstimation = null;
        private CalculationBy_TransferClass CalculationGroup = null;
        private List<PNCListData> PNCList = null;
        private List<PlusMinusModel> ItemsList_ANCChange = null;
        private List<int> PlatformList_ANCSpecial;
        private List<PNCSpecialModel> PNCSpecialItems;
        private List<CalculationModels> FinalCalculation;
        #endregion

        public bool Save()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Mediator.Mediator.NotifyColleagues("General_Information_Save", _GeneralInformation);
            Mediator.Mediator.NotifyColleagues("GetPlatform", _Platform);
            Mediator.Mediator.NotifyColleagues("Get_ANCChange", _ANCList);
            Mediator.Mediator.NotifyColleagues("Get_ECCC", _ECCC);
            Mediator.Mediator.NotifyColleagues("QuantityPercent_Model", _QEstimation);
            Mediator.Mediator.NotifyColleagues("Get_Class", CalculationGroup);
            Mediator.Mediator.NotifyColleagues("PNC_Models", PNCList);
            Mediator.Mediator.NotifyColleagues("Get_ANCSpecial_PLusMinus", ItemsList_ANCChange);
            Mediator.Mediator.NotifyColleagues("ANCSpecial_Platform", PlatformList_ANCSpecial);
            Mediator.Mediator.NotifyColleagues("PNCSpecial_Save_Object", PNCSpecialItems);
            Mediator.Mediator.NotifyColleagues("Tables_Model", FinalCalculation);


            if (_GeneralInformation.ID == 0)
            {
                _GeneralInformation.ActionID = Action_Controller.NewActionNumber();
            }
            else
            {
                OryginalAction = Action_Controller.Load(_GeneralInformation.ActionID);
                OryginalAction.ActiveAction = false;
            }

            UpdatedAction = new Action_DB();
            Prepare();

            Mouse.OverrideCursor = null;
            return true;
        }

        private void Prepare()
        {
            GeneralInformation_Update();

            Devisions_Update();
            ActionPlatform_Update();
            Plant_Update();
            ActionLeader_Update();
            Tag_Update();

            ANCChange_Update();

            switch(CalculationGroup.Grup)
            {
                case 2:
                    ANCSpecial_Update();
                    break;
                case 3:
                    PNC_Update();
                    break;
                case 4:
                    PNCSpecial_Update();
                    break;
                default:
                    break;
            }

            Results_Update();
            UpdatedAction.CreateBy = Environment.UserName.ToLower();
            UpdatedAction.DateBy = DateTime.UtcNow;
            UpdatedAction.ActiveAction = true;
        }

        private void Results_Update()
        {
            var FinalCalc_Good = FinalCalculation.Where(b => b.Update == true).ToList();
            foreach(var Record in FinalCalc_Good)
            {
                var NewRecord = new Calculation_DB()
                {
                    ID = Record.ID,
                    Item = Record.Item,
                    Revision = Record.Revision,
                    Month = Record.Month,
                    CarryOver = Record.CarryOver,
                    Quantity = Record.Quantity,
                    Savings = Record.Savings,
                    ECCC = Record.ECCC,
                };
                var NewConnection = new Action_Results_InterTable()
                {
                    Action = UpdatedAction,
                    Result = NewRecord,
                };
                UpdatedAction.Action_Results.Add(NewConnection);
            }
        }

        private void PNCSpecial_Update()
        {
            foreach (var PNCRecord in PNCSpecialItems)
            {
                var NewPNCRecord = new PNCSpecial_PNC_DB()
                {
                    PNC = PNCRecord.PNC,
                    ECCC = PNCRecord.ECCC,
                    Old_STK = PNCRecord.Old_STK,
                    New_STK = PNCRecord.New_STK,
                    Delta = PNCRecord.Delta,
                };
                foreach (var ANCRecord in PNCRecord.ANCChange)
                {
                    var NewANCRecord = new PNCSpecial_ANC_DB()
                    {
                        Old_ANC = ANCRecord.Old_ANC,
                        Old_Q = ANCRecord.Old_Q,
                        Old_STK = ANCRecord.Old_STK,
                        New_ANC = ANCRecord.New_ANC,
                        New_Q = ANCRecord.New_Q,
                        New_STK = ANCRecord.New_STK,
                        Delta = ANCRecord.Delta,
                    };

                    var NewPNCANCInter = new PNCSPecial_PNC_ANC_InterTable()
                    {
                        PNCChange = NewPNCRecord,
                        ANCChange = NewANCRecord,
                    };
                    NewPNCRecord.PNC_ANC_Special.Add(NewPNCANCInter);
                }
                var NewPNCInterTable = new Action_PNCSpecial_InterTable()
                {
                    Action = UpdatedAction,
                    PNCSpecial = NewPNCRecord,
                };
                UpdatedAction.Action_PNCSpecial.Add(NewPNCInterTable);
            }
        }

        private void PNC_Update()
        {
            foreach (var PNCRow in PNCList)
            {
                var NewRecordPNC = new CalcByPNC()
                {
                    ID = PNCRow.ID,
                    PNC = PNCRow.PNC,
                };
                var NewRecord = new Action_PNC_InterTable()
                {
                    Action = UpdatedAction,
                    List = NewRecordPNC,
                };
                UpdatedAction.Action_PNC.Add(NewRecord);
            }
        }

        private void ANCSpecial_Update()
        {
            foreach(var Item in ItemsList_ANCChange)
            {
                var NewObject = new ANCSpecial_ByItems_DB()
                {
                    Item = Item.Item,
                    Plus = Item.Plus,
                    Minus = Item.Minus,
                };
                var NewObjectInter = new Action_ANCChange_Items_InterTable()
                {
                    Action = UpdatedAction,
                    Item = NewObject,
                };
                UpdatedAction.Action_ANCChange_Items.Add(NewObjectInter);
            }

            foreach(var Record in PlatformList_ANCSpecial)
            {
                var NewRecord = new Action_ANCChangePlatform_InterTable()
                {
                    Action = UpdatedAction,
                    ChangeID = Record,
                };
                UpdatedAction.Action_ANCChange_Platform.Add(NewRecord);
            }
        }

        private void ANCChange_Update()
        {
            var ANCChangeList = new List<ANCChange_DB>();
            foreach (var ANC in _ANCList)
            {
                var NewRecord = new ANCChange_DB()
                {
                    OldANC = ANC.OldANC,
                    OldANCQuantity = ANC.OldANCQuantity,
                    OldSTK = ANC.OldSTK,
                    NewANC = ANC.NewANC,
                    NewANCQuantity = ANC.NewANCQuantity,
                    NewSTK = ANC.NewSTK,
                    Delta = ANC.Delta,
                    UserEstymacja = ANC.UserEstymacja,
                    Percent = ANC.Percent,
                    Estymacja = ANC.Estymacja,
                    NextANC1 = ANC.NextANC1,
                    NextANC2 = ANC.NextANC2,
                };
                var NewInterRecord = new Action_ANCChage_InterTable
                {
                    Action = UpdatedAction,
                    ANCChange = NewRecord,
                };
                UpdatedAction.Action_ANCChange.Add(NewInterRecord);
            }
        }

        private void Tag_Update()
        {
            var NewTag = new Action_Tag_InterTable
            {
                Action = UpdatedAction,
                TagID = _GeneralInformation.Tag.ID,
            };
            UpdatedAction.Action_Tag.Add(NewTag);
        }

        private void ActionLeader_Update()
        {
            var NewActionLeader = new Action_Leader_InterTable
            {
                Action = UpdatedAction,
                LeaderID = _GeneralInformation.Leader.LeaderID,
            };
            UpdatedAction.Action_Leader.Add(NewActionLeader);
        }

        private void Plant_Update()
        {
            var NewPlant = new Action_Plant_InterTable
            {
                Action = UpdatedAction,
                PlantID = _GeneralInformation.Plant.PlantID,
            };
            UpdatedAction.Action_Plant.Add(NewPlant);
        }

        private void Devisions_Update()
        {
            var NewDevision = new Action_Devision_InterTable
            {
                Action = UpdatedAction,
                DevisionID = _GeneralInformation.Devision.DevisionID,
            };
            UpdatedAction.Action_Devision.Add(NewDevision);
        }

        private void ActionPlatform_Update()
        {
            UpdatedAction.DMD = _Platform.DMD;
            UpdatedAction.D45 = _Platform.D45;
            UpdatedAction.FS = _Platform.FS;
            UpdatedAction.FI = _Platform.FI;
            UpdatedAction.BI = _Platform.BI;
            UpdatedAction.FSBU = _Platform.FSBU;
        }

        private void GeneralInformation_Update()
        {
            UpdatedAction.ID = 0;
            UpdatedAction.ActionID = _GeneralInformation.ActionID;
            UpdatedAction.Name = _GeneralInformation.Name;
            UpdatedAction.Description = _GeneralInformation.Description;
            UpdatedAction.StartYear = _GeneralInformation.Month;
            UpdatedAction.Month = _GeneralInformation.Month;
            UpdatedAction.Active = _GeneralInformation.Active;
            UpdatedAction.ECCC = _ECCC.ECCC;
            UpdatedAction.ECCCSpec = _ECCC.ECCCSpecial;
            UpdatedAction.ECCCValue = _ECCC.ECCC_Value;
            UpdatedAction.QEstymation = _QEstimation.Percent;
            UpdatedAction.Calculation = CalculationGroup.Grup;
        }

    }
}
