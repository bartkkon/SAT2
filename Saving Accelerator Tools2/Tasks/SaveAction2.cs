using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.Tasks
{
    public class SaveAction2
    {
        #region Constructors
        public SaveAction2()
        {
            Mediator.Mediator.Register("Set_GeneralInformation", GeneralInformation);
            Mediator.Mediator.Register("Set_Platform", GetPlatform);
            Mediator.Mediator.Register("Set_ANCList", SetANCList);
            Mediator.Mediator.Register("ECCC_Save", ECCC_ActionData);
            Mediator.Mediator.Register("Q_Estymation_Save", QEstymation);
            Mediator.Mediator.Register("Set_CalcGroupForSave", CalcGroup);
            Mediator.Mediator.Register("Set_PNCList_Save", PNC_List);
            Mediator.Mediator.Register("ANCChange_Items", ANCChange_Items);
            Mediator.Mediator.Register("ANCSpecial_Platform", ANCSpecial_Platforms);
            Mediator.Mediator.Register("PNCSpecial_Items", PNCSpecial_Items);
        }
        ~SaveAction2()
        {
            Mediator.Mediator.Unregister("Set_GeneralInformation", GeneralInformation);
            Mediator.Mediator.Unregister("Set_Platform", GetPlatform);
            Mediator.Mediator.Unregister("Set_ANCList", SetANCList);
            Mediator.Mediator.Unregister("ECCC_Save", ECCC_ActionData);
            Mediator.Mediator.Unregister("Q_Estymation_Save", QEstymation);
            Mediator.Mediator.Unregister("Set_CalcGroupForSave", CalcGroup);
            Mediator.Mediator.Unregister("Set_PNCList_Save", PNC_List);
            Mediator.Mediator.Unregister("ANCChange_Items", ANCChange_Items);
            Mediator.Mediator.Unregister("ANCSpecial_Platform", ANCSpecial_Platforms);
            Mediator.Mediator.Unregister("PNCSpecial_Items", PNCSpecial_Items);
        }
        #endregion

        #region Private Variables
        private bool AnyUpdated = false;
        private Action_DB ActionSave;
        private General_Information_Model _GeneralInformation = null;
        private PlatformModel _Platform = null;
        private List<ANCChangeModel> _ANCList;
        private DataBaseConnetionContext context;
        private ECCCModel _ECCC;
        private decimal QEstymation_Value;
        private int CalculationGroup;
        private List<PNCListData> PNCList;
        private List<PlusMinusModel> ItemsList_ANCChange;
        private List<int> PlatformList_AnCSpecial;
        private List<PNCSpecialModel> PNCSpecialItems;
        #endregion

        #region Main Save Function
        public bool Save()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            do
            {
                Mediator.Mediator.NotifyColleagues("General_Information_Save", null);
                Mediator.Mediator.NotifyColleagues("GetPlatform", null);
                Mediator.Mediator.NotifyColleagues("Get_ANCChange", null);
                Mediator.Mediator.NotifyColleagues("Get_ECCC", null);
                Mediator.Mediator.NotifyColleagues("Get_QuantityEstimation", null);
                Mediator.Mediator.NotifyColleagues("Get_Calc", null);
                Mediator.Mediator.NotifyColleagues("Get_PNC_Data", null);
                Mediator.Mediator.NotifyColleagues("Get_ANCSpecial_PLusMinus", null);
                Mediator.Mediator.NotifyColleagues("Get_ANCSpecial_Platform", null);
                Mediator.Mediator.NotifyColleagues("PNCSpecial_Save", null);
                Thread.Sleep(200);
            } while (_GeneralInformation == null
            && _Platform == null
            && _ANCList == null
            && _ECCC == null
            && QEstymation_Value == 0
            && CalculationGroup != 0
            && ItemsList_ANCChange == null
            && PlatformList_AnCSpecial == null
            && PNCSpecialItems == null);

            context = new DataBaseConnetionContext();

            ActionSave = _GeneralInformation.ID == 0 ? new Action_DB() : Action_Controller.Load(_GeneralInformation.ActionID, context);

            GeneralInformation_Update();

            Devision_Update();
            Plant_Update();
            Leader_Update();
            Tag_Update();
            ANC_Update();
            CalculationBy_Manegement();

            if (AnyUpdated)
            {
                context.Action.Update(ActionSave);
                context.SaveChanges();
            }

            Mouse.OverrideCursor = null;
            return true;
        }
        #endregion

        #region Functions
        private void GeneralInformation_Update()
        {
            ActionSave.ActionID = ActionSave.ActionID == 0 ? Action_Controller.NewActionNumber() : _GeneralInformation.ActionID;
            if (!ActionSave.Name.Equals(_GeneralInformation.Name)) { ActionSave.Name = _GeneralInformation.Name; AnyUpdated = true; }
            if (!ActionSave.Description.Equals(_GeneralInformation.Description)) { ActionSave.Description = _GeneralInformation.Description; AnyUpdated = true; }
            if (ActionSave.StartYear != _GeneralInformation.StartYear) { ActionSave.StartYear = _GeneralInformation.StartYear; AnyUpdated = true; }
            if (ActionSave.Month != _GeneralInformation.Month) { ActionSave.Month = _GeneralInformation.Month; AnyUpdated = true; }
            if (ActionSave.Active != _GeneralInformation.Active) { ActionSave.Active = _GeneralInformation.Active; AnyUpdated = true; }

            //Platforms
            if (ActionSave.DMD != _Platform.DMD) { ActionSave.DMD = _Platform.DMD; AnyUpdated = true; }
            if (ActionSave.D45 != _Platform.D45) { ActionSave.D45 = _Platform.D45; AnyUpdated = true; }
            if (ActionSave.FS != _Platform.FS) { ActionSave.FS = _Platform.FS; AnyUpdated = true; }
            if (ActionSave.FI != _Platform.FI) { ActionSave.FI = _Platform.FI; AnyUpdated = true; }
            if (ActionSave.BI != _Platform.BI) { ActionSave.BI = _Platform.BI; AnyUpdated = true; }
            if (ActionSave.FSBU != _Platform.FSBU) { ActionSave.FSBU = _Platform.FSBU; AnyUpdated = true; }

            //ECCC
            if (ActionSave.ECCC != _ECCC.ECCC) { ActionSave.ECCC = _ECCC.ECCC; AnyUpdated = true; }
            if (ActionSave.ECCCSpec != _ECCC.ECCCSpecial) { ActionSave.ECCCSpec = _ECCC.ECCCSpecial; AnyUpdated = true; }
            if (ActionSave.ECCCValue != _ECCC.ECCC_Value) { ActionSave.ECCCValue = _ECCC.ECCC_Value; AnyUpdated = true; }

            //Quantity Percent
            if (ActionSave.QEstymation != QEstymation_Value) { ActionSave.QEstymation = QEstymation_Value; AnyUpdated = true; }
        }
        private void Devision_Update()
        {
            if (ActionSave.Action_Devision.Any(d => d.DevisionID != _GeneralInformation.Devision.DevisionID))
            {
                var DeletedList = ActionSave.Action_Devision.ToList();
                if (DeletedList.Count != 0)
                {
                    foreach (var DeletedItem in DeletedList)
                    {
                        ActionSave.Action_Devision.Remove(DeletedItem);
                    }
                }
                if (ActionSave.Action_Devision.Count == 0)
                {
                    var AddDevision = new Action_Devision_InterTable
                    {
                        Action = ActionSave,
                        DevisionID = _GeneralInformation.Devision.DevisionID,
                    };
                    ActionSave.Action_Devision.Add(AddDevision);
                    AnyUpdated = true;
                }
            }
        }
        private void Plant_Update()
        {
            if (ActionSave.Action_Plant.Any(p => p.PlantID != _GeneralInformation.Plant.PlantID))
            {
                var DeletedList = ActionSave.Action_Plant.ToList();
                if (DeletedList.Count != 0)
                {
                    foreach (var DeletedItem in DeletedList)
                    {
                        ActionSave.Action_Plant.Remove(DeletedItem);
                    }
                }
                if (ActionSave.Action_Plant.Count == 0)
                {
                    var AddPlant = new Action_Plant_InterTable
                    {
                        Action = ActionSave,
                        PlantID = _GeneralInformation.Plant.PlantID,
                    };
                    ActionSave.Action_Plant.Add(AddPlant);
                    AnyUpdated = true;
                }
            }
        }
        private void Leader_Update()
        {
            if (ActionSave.Action_Leader.Any(l => l.LeaderID != _GeneralInformation.Leader.LeaderID))
            {
                var DeletedList = ActionSave.Action_Leader.ToList();
                if (DeletedList.Count != 0)
                {
                    foreach (var DeletedItem in DeletedList)
                    {
                        ActionSave.Action_Leader.Remove(DeletedItem);
                    }
                }
                if (ActionSave.Action_Leader.Count == 0)
                {
                    var AddLeader = new Action_Leader_InterTable
                    {
                        Action = ActionSave,
                        LeaderID = _GeneralInformation.Leader.LeaderID,
                    };
                    ActionSave.Action_Leader.Add(AddLeader);
                    AnyUpdated = true;
                }
            }
        }
        private void Tag_Update()
        {
            if (ActionSave.Action_Tag.Any(t => t.TagID != _GeneralInformation.Tag.ID))
            {
                var DeletedList = ActionSave.Action_Tag.ToList();
                if (DeletedList.Count != 0)
                {
                    foreach (var DeletedItem in DeletedList)
                    {
                        ActionSave.Action_Tag.Remove(DeletedItem);
                    }
                }
                if (ActionSave.Action_Tag.Count == 0)
                {
                    var AddTag = new Action_Tag_InterTable
                    {
                        Action = ActionSave,
                        TagID = _GeneralInformation.Tag.ID,
                    };
                    ActionSave.Action_Tag.Add(AddTag);
                    AnyUpdated = true;
                }
            }
        }
        private void ANC_Update()
        {
            var UpdateList = new List<ANCChange_DB>();
            var ANCChange = ActionSave.Action_ANCChange.ToList();

            foreach (var ANC in ANCChange)
            {
                if (!_ANCList.Any(a => a.ID == ANC.ANCChangeID))
                {
                    ActionSave.Action_ANCChange.Remove(ANC);
                }
            }

            foreach (var ANCItem in _ANCList)
            {

                var Record = new ANCChange_DB()
                {
                    ID = ANCItem.ID,
                    OldANC = ANCItem.OldANC,
                    OldANCQuantity = ANCItem.OldANCQuantity,
                    OldSTK = ANCItem.OldSTK,
                    NewANC = ANCItem.NewANC,
                    NewANCQuantity = ANCItem.NewANCQuantity,
                    NewSTK = ANCItem.NewSTK,
                    Delta = ANCItem.Delta,
                    UserEstymacja = ANCItem.UserEstymacja,
                    Percent = ANCItem.Percent,
                    Estymacja = ANCItem.Estymacja,
                    NextANC1 = ANCItem.NextANC1,
                    NextANC2 = ANCItem.NextANC2,
                };
                var NewRecord = new Action_ANCChage_InterTable
                {
                    Action = ActionSave,
                    ANCChange = Record,
                };
                if (Record.ID == 0)
                {
                    ActionSave.Action_ANCChange.Add(NewRecord);
                }
                else
                {
                    var FindRecord = ActionSave.Action_ANCChange.Where(a => a.ANCChangeID == Record.ID).First();
                    if (!FindRecord.ANCChange.Equals(Record))
                    {
                        UpdateList.Add(Record);
                    }
                }
            }
            if (UpdateList.Count != 0)
            {
                ANCChange_Controller.Update(UpdateList);
            }
        }
        private void CalculationBy_Manegement()
        {
            if (ActionSave.Calculation != CalculationGroup && ActionSave.Calculation != 0)
            {
                switch (ActionSave.Calculation)
                {
                    case 1:

                        break;
                    case 2:
                        ANCSpecial_RemoveGroup();
                        break;
                    case 3:
                        PNC_RemoveList();
                        break;
                    case 4:
                        PNCSpecial_RemoveGroup();
                        break;
                }
                ActionSave.Calculation = CalculationGroup;
            }

            switch (ActionSave.Calculation)
            {
                case 1:

                    break;
                case 2:
                    ANCSpecial_Update();
                    break;
                case 3:
                    PNC_Update();
                    break;
                case 4:
                    PNCSpecial_Update();
                    break;
            }
        }
        private void ANCSpecial_RemoveGroup()
        {
            var DeletedListPlatform = ActionSave.Action_ANCChange_Platform.ToList();

            foreach(var DeletedItem in DeletedListPlatform)
            {
                ActionSave.Action_ANCChange_Platform.Remove(DeletedItem);
                AnyUpdated = true;
            }

            var DeletedListItem = ActionSave.Action_ANCChange_Items.ToList();
            foreach(var DeletedItem in DeletedListItem)
            {
                ActionSave.Action_ANCChange_Items.Remove(DeletedItem);
                AnyUpdated = true;
            }
        }
        private void ANCSpecial_Update()
        {
            //Itemy plus minus
            foreach (var Record in ItemsList_ANCChange)
            {
                if (Record.ID == 0)
                {
                    var NewObject = new ANCSpecial_ByItems_DB()
                    {
                        Item = Record.Item,
                        Plus = Record.Plus,
                        Minus = Record.Minus,
                    };
                    var NewRecord = new Action_ANCChange_Items_InterTable()
                    {
                        Action = ActionSave,
                        Item = NewObject,
                    };
                    ActionSave.Action_ANCChange_Items.Add(NewRecord);
                    AnyUpdated = true;
                }
                else if (Record.Active == false)
                {
                    var Deletedrecord = ActionSave.Action_ANCChange_Items.Where(c => c.ItemID == Record.ID).FirstOrDefault();
                    if (Deletedrecord != null)
                    {
                        ActionSave.Action_ANCChange_Items.Remove(Deletedrecord);
                        AnyUpdated = true;
                    }
                }
            }
            //Platformy
            foreach (var Record in PlatformList_AnCSpecial)
            {
                if (!ActionSave.Action_ANCChange_Platform.Any(c => c.ChangeID == Record))
                {
                    var NewRecord = new Action_ANCChangePlatform_InterTable()
                    {
                        Action = ActionSave,
                        ChangeID = Record,
                    };
                    ActionSave.Action_ANCChange_Platform.Add(NewRecord);
                    AnyUpdated = true;
                }
            }
            List<int> ToDeleted = new List<int>();
            foreach (var Record in ActionSave.Action_ANCChange_Platform)
            {
                if (!PlatformList_AnCSpecial.Any(c => c == Record.ChangeID))
                {
                    ToDeleted.Add(Record.ChangeID);
                }
            }
            foreach (var Record in ToDeleted)
            {
                ActionSave.Action_ANCChange_Platform.Remove(ActionSave.Action_ANCChange_Platform.Where(b => b.ChangeID == Record).First());
                AnyUpdated = true;
            }
        }
        private void PNC_RemoveList()
        {
            var DeletedList = ActionSave.Action_PNC.ToList();

            if (DeletedList.Count != 0)
            {
                foreach (var DeletedItem in DeletedList)
                {
                    ActionSave.Action_PNC.Remove(DeletedItem);
                    AnyUpdated = true;
                }
            }
        }
        private void PNC_Update()
        {
            foreach (var PNCItem in PNCList)
            {
                if (!ActionSave.Action_PNC.Any(c => c.List.PNC == PNCItem.PNC))
                {
                    var NewRecordPNC = new CalcByPNC()
                    {
                        ID = 0,
                        PNC = PNCItem.PNC,
                    };
                    var NewRecord = new Action_PNC_InterTable()
                    {
                        ActionID = ActionSave.ID,
                        List = NewRecordPNC,
                    };
                    ActionSave.Action_PNC.Add(NewRecord);
                    AnyUpdated = true;
                }
            }

            var DeletedList = new List<Action_PNC_InterTable>();
            foreach (var CheckRecord in ActionSave.Action_PNC)
            {
                if (!PNCList.Any(p => p.PNC == CheckRecord.List.PNC))
                {
                    DeletedList.Add(CheckRecord);
                }
            }
            if (DeletedList.Count != 0)
            {
                foreach (var DeletedItem in DeletedList)
                {
                    ActionSave.Action_PNC.Remove(DeletedItem);
                    AnyUpdated = true;
                }
            }
        }
        private void PNCSpecial_RemoveGroup()
        {
            var DeletedList = ActionSave.Action_PNCSpecial.ToList();

            if (DeletedList.Count != 0)
            {
                foreach (var DeletedItem in DeletedList)
                {
                    ActionSave.Action_PNCSpecial.Remove(DeletedItem);
                    AnyUpdated = true;
                }
            }
        }
        private void PNCSpecial_Update()
        {
            foreach (var PNCRecord in PNCSpecialItems)
            {
                if(PNCRecord.ID == 0)
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
                        Action = ActionSave,
                        PNCSpecial = NewPNCRecord,
                    };
                    ActionSave.Action_PNCSpecial.Add(NewPNCInterTable);
                    AnyUpdated = true;
                }
            }

            List<int> DeleteList = new List<int>();
            foreach (var BaseList in ActionSave.Action_PNCSpecial)
            {
                if (!PNCSpecialItems.Any(c => c.ID == BaseList.PNCSpecID))
                {
                    DeleteList.Add(BaseList.PNCSpecID);
                }
            }
            foreach (var Delete in DeleteList)
            {
                ActionSave.Action_PNCSpecial.Remove(ActionSave.Action_PNCSpecial.Where(b => b.PNCSpecID == Delete).First());
                AnyUpdated = true;
            }
        }
        #endregion

        #region Mediators Services
        private void GeneralInformation(object sent)
        {
            _GeneralInformation = sent as General_Information_Model;
        }
        private void GetPlatform(object sent)
        {
            _Platform = sent as PlatformModel;
        }
        private void SetANCList(object sent)
        {
            _ANCList = sent as List<ANCChangeModel>;
        }
        private void ECCC_ActionData(object sent)
        {
            _ECCC = sent as ECCCModel;
        }
        private void QEstymation(object sent)
        {
            QEstymation_Value = (decimal)sent;
        }
        private void CalcGroup(object sent)
        {
            CalculationGroup = (int)sent;
        }
        private void ANCChange_Items(object sent)
        {
            ItemsList_ANCChange = sent as List<PlusMinusModel>;
        }
        private void ANCSpecial_Platforms(object sent)
        {
            PlatformList_AnCSpecial = sent as List<int>;
        }
        private void PNCSpecial_Items(object sent)
        {
            PNCSpecialItems = sent as List<PNCSpecialModel>;
        }
        private void PNC_List(object sent)
        {
            PNCList = sent as List<PNCListData>;
        }
        #endregion
    }
}
