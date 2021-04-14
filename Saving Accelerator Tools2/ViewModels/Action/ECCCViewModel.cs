using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class ECCCViewModel : INotifyProperty
    {
        #region Constructors
        public ECCCViewModel()
        {
            Mediator.Mediator.Register("Set_ECCC", Set_ECCC);
            Mediator.Mediator.Register("Get_ECCC", Get_ECCC);
            Mediator.Mediator.Register("Get_ECCCModel", Get_ECCCModel);
            Mediator.Mediator.Register("ECCCSpec", ECCC_Spec_Toggle);
        }
        ~ECCCViewModel()
        {
            Mediator.Mediator.Unregister("Set_ECCC", Set_ECCC);
            Mediator.Mediator.Unregister("Get_ECCC", Get_ECCC);
            Mediator.Mediator.Unregister("Get_ECCCModel", Get_ECCCModel);
            Mediator.Mediator.Unregister("ECCCSpec", ECCC_Spec_Toggle);
        }
        #endregion

        #region Privates Variables
        private bool _ECCC;
        private bool _ECCC_Spec;
        private Visibility _ECCC_Spec_Visibility = Visibility.Hidden;
        private decimal _ECCC_Value;
        private bool _ECCC_Value_Enabled;

        #endregion

        #region Public Variables
        public bool ECCC
        {
            get { return _ECCC; }
            set
            {
                _ECCC = value;
                ECC_Value_CheckVisibilit();
                RisePropoertyChanged();
            }
        }
        public bool ECCC_Spec
        {
            get { return _ECCC_Spec; }
            set
            {
                _ECCC_Spec = value;
                ECC_Value_CheckVisibilit();
                RisePropoertyChanged();
            }
        }
        public Visibility ECCC_Spec_Visibility
        {
            get { return _ECCC_Spec_Visibility; }
            set
            {
                _ECCC_Spec_Visibility = value;
                RisePropoertyChanged();
            }
        }
        public decimal ECCC_Value
        {
            get { return _ECCC_Value; }
            set
            {
                _ECCC_Value = value;
                RisePropoertyChanged();
            }
        }
        public bool ECCC_Value_Enabled
        {
            get { return _ECCC_Value_Enabled; }
            set
            {
                _ECCC_Value_Enabled = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Functions
        private void ECC_Value_CheckVisibilit()
        {
            if(!_ECCC)
            {
                ECCC_Value_Enabled = false;
                ECCC_Value = 0;
            }
            else
            {
                if(_ECCC_Spec)
                {
                    ECCC_Value_Enabled = false;
                    ECCC_Value = 0;
                }
                else
                {
                    ECCC_Value_Enabled = true;
                }
            }
        }
        #endregion

        #region Mediator Functions
        private void Set_ECCC(object obj)
        {
            var IncomingModel = obj as ECCCModel;
            ECCC = IncomingModel.ECCC;
            ECCC_Spec = IncomingModel.ECCCSpecial;
            ECCC_Value = IncomingModel.ECCC_Value;
        }
        private void Get_ECCC(object obj)
        {
            ECCCModel OutModel = new ECCCModel()
            {
                ECCC = ECCC,
                ECCCSpecial = ECCC_Spec,
                ECCC_Value = ECCC_Value,
            };
            Mediator.Mediator.NotifyColleagues("ECCC_Save", OutModel);
        }
        private void ECCC_Spec_Toggle(object obj)
        {
            if((bool)obj)
            {
                ECCC_Spec_Visibility = Visibility.Visible;
            }
            else
            {
                ECCC_Spec_Visibility = Visibility.Hidden;
            }
        }
        private void Get_ECCCModel(object obj)
        {
            (obj as ECCCModel).ECCC = ECCC;
            (obj as ECCCModel).ECCCSpecial = ECCC_Spec;
            (obj as ECCCModel).ECCC_Value = ECCC_Value;
        }
        #endregion
    }
}
