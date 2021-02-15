using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;
using Saving_Accelerator_Tools2.Mediator;
using System.Windows.Input;
using System.Windows;

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

            #endregion

            #region Sent information to mediator    
            Mediator.Mediator.NotifyColleagues("General_Information_Load", GeneralInformation);
            Mediator.Mediator.NotifyColleagues("SetPlatform", Platform);
            Mediator.Mediator.NotifyColleagues("Set_ANCChange", ANCChange);
            Mediator.Mediator.NotifyColleagues("Set_ECCC", ECCCValue);
            Mediator.Mediator.NotifyColleagues("Set_QuantityEstymation", ActionDataBase.QEstymation);

            #endregion
            Mouse.OverrideCursor = null;
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
            foreach(var ANCRow in ActionDataBase.Action_ANCChange)
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
    }
}
