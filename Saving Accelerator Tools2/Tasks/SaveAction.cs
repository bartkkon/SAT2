using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Models.Action;
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
        private bool AnyUpdated = false;
        private Action_DB SaveToAction;
        private General_Information_Model _GeneralInformation = null;
        private PlatformModel _Platform = null;
        private List<ANCChangeModel> _ANCList;
        private DataBaseConnetionContext context;
        private ECCCModel _ECCC;
        private decimal QEstymation_Value;
        private int CalculationGroup;
        private List<PNCListData> PNCList;

        public SaveAction()
        {
            Mediator.Mediator.Register("Set_GeneralInformation", GeneralInformation);
            Mediator.Mediator.Register("Set_Platform", GetPlatform);
            Mediator.Mediator.Register("Set_ANCList", SetANCList);
            Mediator.Mediator.Register("ECCC_Save", ECCC_ActionData);
            Mediator.Mediator.Register("Q_Estymation_Save", QEstymation);
            Mediator.Mediator.Register("Set_CalcGroupForSave", CalcGroup);
            Mediator.Mediator.Register("Set_PNCList_Save", CalcGroup);
        }

        ~SaveAction()
        {
            Mediator.Mediator.Unregister("Set_GeneralInformation", GeneralInformation);
            Mediator.Mediator.Unregister("Set_Platform", GetPlatform);
            Mediator.Mediator.Unregister("Set_ANCList", SetANCList);
            Mediator.Mediator.Unregister("ECCC_Save", ECCC_ActionData);
            Mediator.Mediator.Unregister("Q_Estymation_Save", QEstymation);
            Mediator.Mediator.Unregister("Set_CalcGroupForSave", CalcGroup);
            Mediator.Mediator.Unregister("Set_PNCList_Save", CalcGroup);
        }

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
                Thread.Sleep(1000);
            } while (_GeneralInformation == null
            && _Platform == null
            && _ANCList == null
            && _ECCC == null
            && QEstymation_Value == 0
            && CalculationGroup != 0);

            if (_GeneralInformation.ID == 0)
            {
                SaveToAction = new Action_DB();
                PrepareNewAction();
            }
            else
            {
                context = new DataBaseConnetionContext();
                SaveToAction = Action_Controller.Load(_GeneralInformation.ActionID, context);
                PreapreUpdateAction();
            }

            Mouse.OverrideCursor = null;
            return true;
        }

        private void PrepareNewAction()
        {
            SaveToAction.ID = _GeneralInformation.ID;
            SaveToAction.ActionID = Action_Controller.NewActionNumber();
            SaveToAction.Name = _GeneralInformation.Name;
            SaveToAction.Description = _GeneralInformation.Description;
            SaveToAction.StartYear = _GeneralInformation.StartYear;
            SaveToAction.Month = _GeneralInformation.Month;
            SaveToAction.Active = _GeneralInformation.Active;
            SaveToAction.ECCC = _ECCC.ECCC;
            SaveToAction.ECCCSpec = _ECCC.ECCCSpecial;
            SaveToAction.ECCCValue = _ECCC.ECCC_Value;
            SaveToAction.QEstymation = QEstymation_Value;
            SaveToAction.Calculation = CalculationGroup;

            //Zapis Akcji aby dostać ID potrzebne przy InterTable
            Action_Controller.NewAction(SaveToAction);


            var NewDevision = new Action_Devision_InterTable
            {
                ActionID = SaveToAction.ID,
                DevisionID = _GeneralInformation.Devision.DevisionID,
            };
            SaveToAction.Action_Devision.Add(NewDevision);

            var NewPlant = new Action_Plant_InterTable
            {
                ActionID = SaveToAction.ID,
                PlantID = _GeneralInformation.Plant.PlantID,
            };
            SaveToAction.Action_Plant.Add(NewPlant);

            var NewLeader = new Action_Leader_InterTable
            {
                ActionID = SaveToAction.ID,
                LeaderID = _GeneralInformation.Leader.LeaderID,
            };
            SaveToAction.Action_Leader.Add(NewLeader);

            var NewTag = new Action_Tag_InterTable
            {
                ActionID = SaveToAction.ID,
                TagID = _GeneralInformation.Tag.ID,
            };
            SaveToAction.Action_Tag.Add(NewTag);

            foreach (var ANC in _ANCList)
            {
                var newRecordANC = new ANCChange_DB()
                {
                    ID = ANC.ID,
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
                var NewRecord = new Action_ANCChage_InterTable()
                {
                    ActionID = SaveToAction.ID,
                    ANCChange = newRecordANC,
                };
                SaveToAction.Action_ANCChange.Add(NewRecord);
            }

            if (CalculationGroup == 3)
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
                        ActionID = SaveToAction.ID,
                        List = NewRecordPNC,
                    };
                    SaveToAction.Action_PNC.Add(NewRecord);
                }
            }

            Action_Controller.UpdateAction(SaveToAction);
        }

        private void PreapreUpdateAction()
        {


            if (SaveToAction.ActionID != _GeneralInformation.ActionID)
            {
                SaveToAction.ActionID = _GeneralInformation.ActionID;
                AnyUpdated = true;
            }
            if (SaveToAction.Name != _GeneralInformation.Name)
            {
                SaveToAction.Name = _GeneralInformation.Name;
                AnyUpdated = true;
            }
            if (SaveToAction.Description != _GeneralInformation.Description)
            {
                SaveToAction.Description = _GeneralInformation.Description;
                AnyUpdated = true;
            }
            if (SaveToAction.StartYear != SaveToAction.StartYear)
            {
                SaveToAction.StartYear = _GeneralInformation.StartYear;
                AnyUpdated = true;
            }
            if (SaveToAction.Month != _GeneralInformation.Month)
            {
                SaveToAction.Month = _GeneralInformation.Month;
                AnyUpdated = true;
            }
            if (SaveToAction.Active != _GeneralInformation.Active)
            {
                SaveToAction.Active = _GeneralInformation.Active;
                AnyUpdated = true;
            }
            if (SaveToAction.DMD != _Platform.DMD)
            {
                SaveToAction.DMD = _Platform.DMD;
                AnyUpdated = true;
            }
            if (SaveToAction.D45 != _Platform.D45)
            {
                SaveToAction.D45 = _Platform.D45;
                AnyUpdated = true;
            }
            if (SaveToAction.FS != _Platform.FS)
            {
                SaveToAction.FS = _Platform.FS;
                AnyUpdated = true;
            }
            if (SaveToAction.FI != _Platform.FI)
            {
                SaveToAction.FI = _Platform.FI;
                AnyUpdated = true;
            }
            if (SaveToAction.BI != _Platform.BI)
            {
                SaveToAction.BI = _Platform.BI;
                AnyUpdated = true;
            }
            if (SaveToAction.FSBU != _Platform.FSBU)
            {
                SaveToAction.FSBU = _Platform.FSBU;
                AnyUpdated = true;
            }
            if (SaveToAction.ECCC != _ECCC.ECCC)
            {
                SaveToAction.ECCC = _ECCC.ECCC;
                AnyUpdated = true;
            }
            if (SaveToAction.ECCCSpec != _ECCC.ECCCSpecial)
            {
                SaveToAction.ECCCSpec = _ECCC.ECCCSpecial;
                AnyUpdated = true;
            }
            if (SaveToAction.ECCCValue != _ECCC.ECCC_Value)
            {
                SaveToAction.ECCCValue = _ECCC.ECCC_Value;
                AnyUpdated = true;
            }
            if (SaveToAction.QEstymation != QEstymation_Value)
            {
                SaveToAction.QEstymation = QEstymation_Value;
                AnyUpdated = true;
            }
            if (SaveToAction.Calculation != CalculationGroup)
            {
                if (SaveToAction.Calculation == 4)
                {

                }
                else if (SaveToAction.Calculation == 3)
                {
                    RemovePNCList();
                }
                else if (SaveToAction.Calculation == 2)
                {


                }
                SaveToAction.Calculation = CalculationGroup;
                AnyUpdated = true;
            }

            if (SaveToAction.Action_Devision.Count != 0)
            {
                if (SaveToAction.Action_Devision.Any(b => b.DevisionID != _GeneralInformation.Devision.DevisionID))
                {
                    //var context = new DataBaseConnetionContext();
                    context.Remove(SaveToAction.Action_Devision[0]);
                    var NewDevision = new Action_Devision_InterTable
                    {
                        ActionID = SaveToAction.ID,
                        DevisionID = _GeneralInformation.Devision.DevisionID,
                    };
                    SaveToAction.Action_Devision.Add(NewDevision);
                    context.Action.Update(SaveToAction);
                    context.SaveChanges();
                    AnyUpdated = true;
                }
            }
            else
            {
                var NewDevision = new Action_Devision_InterTable
                {
                    ActionID = SaveToAction.ID,
                    DevisionID = _GeneralInformation.Devision.DevisionID,
                };
                SaveToAction.Action_Devision.Add(NewDevision);
                AnyUpdated = true;
            }

            if (SaveToAction.Action_Plant.Count != 0)
            {
                if (SaveToAction.Action_Plant.Any(b => b.PlantID != _GeneralInformation.Plant.PlantID))
                {
                    //var context = new DataBaseConnetionContext();
                    context.Remove(SaveToAction.Action_Plant[0]);
                    var NewPlant = new Action_Plant_InterTable
                    {
                        ActionID = SaveToAction.ID,
                        PlantID = _GeneralInformation.Plant.PlantID,
                    };
                    SaveToAction.Action_Plant.Add(NewPlant);
                    context.Action.Update(SaveToAction);
                    context.SaveChanges();
                    AnyUpdated = true;
                }
            }
            else
            {
                var NewPlant = new Action_Plant_InterTable
                {
                    ActionID = SaveToAction.ID,
                    PlantID = _GeneralInformation.Plant.PlantID,
                };
                SaveToAction.Action_Plant.Add(NewPlant);
                AnyUpdated = true;
            }

            if (SaveToAction.Action_Leader.Count != 0)
            {
                if (SaveToAction.Action_Leader.Any(b => b.LeaderID != _GeneralInformation.Leader.LeaderID))
                {
                    //var context = new DataBaseConnetionContext();
                    context.Remove(SaveToAction.Action_Leader[0]);
                    var NewLeader = new Action_Leader_InterTable
                    {
                        ActionID = SaveToAction.ID,
                        LeaderID = _GeneralInformation.Leader.LeaderID,
                    };
                    SaveToAction.Action_Leader.Add(NewLeader);
                    context.Action.Update(SaveToAction);
                    context.SaveChanges();
                    AnyUpdated = true;
                }
            }
            else
            {
                var NewLeader = new Action_Leader_InterTable
                {
                    ActionID = SaveToAction.ID,
                    LeaderID = _GeneralInformation.Leader.LeaderID,
                };
                SaveToAction.Action_Leader.Add(NewLeader);
                AnyUpdated = true;
            }

            if (SaveToAction.Action_Tag.Count != 0)
            {
                if (SaveToAction.Action_Tag.Any(b => b.TagID != _GeneralInformation.Tag.ID))
                {
                    //var context = new DataBaseConnetionContext();
                    context.Remove(SaveToAction.Action_Tag[0]);
                    var NewTag = new Action_Tag_InterTable
                    {
                        ActionID = SaveToAction.ID,
                        TagID = _GeneralInformation.Tag.ID,
                    };
                    SaveToAction.Action_Tag.Add(NewTag);
                    context.Action.Update(SaveToAction);
                    context.SaveChanges();
                    AnyUpdated = true;
                }
            }
            else
            {
                var NewTag = new Action_Tag_InterTable
                {
                    ActionID = SaveToAction.ID,
                    TagID = _GeneralInformation.Tag.ID,
                };
                SaveToAction.Action_Tag.Add(NewTag);
                AnyUpdated = true;
            }

            ANCPrepare();

            if (AnyUpdated)
            {
                Action_Controller.UpdateAction(SaveToAction);
            }
        }

        private void ANCPrepare()
        {
            var ActionNewList = new List<ANCChange_DB>();
            foreach (var ANC in _ANCList)
            {
                var newRecord = new ANCChange_DB()
                {
                    ID = ANC.ID,
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
                ActionNewList.Add(newRecord);
            }

            ANCChange_Controller.Update(ActionNewList, ref SaveToAction, ref context);
        }
        private void PNCUpdate()
        {
            foreach (var PNCRecord in PNCList)
            {
                if (!SaveToAction.Action_PNC.Any(c => c.List.PNC == PNCRecord.PNC))
                {
                    var NewRecordPNC = new CalcByPNC()
                    {
                        ID = 0,
                        PNC = PNCRecord.PNC,
                    };
                    var NewRecord = new Action_PNC_InterTable()
                    {
                        ActionID = SaveToAction.ID,
                        List = NewRecordPNC,
                    };
                    SaveToAction.Action_PNC.Add(NewRecord);
                }
            }
            foreach (var PNCCheck in SaveToAction.Action_PNC)
            {
                if (!PNCList.Any(c => c.PNC == PNCCheck.List.PNC))
                {

                }
            }
        }
        private void RemovePNCList()
        {
            foreach (var PNCRecord in SaveToAction.Action_PNC)
            {
                context.Remove(PNCRecord);
            }
            context.SaveChanges();
        }

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
        #endregion
    }
}
