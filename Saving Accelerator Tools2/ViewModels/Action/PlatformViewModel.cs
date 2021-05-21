using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class PlatformViewModel : INotifyProperty
    {
        #region Constructors
        public PlatformViewModel()
        {
            Mediator.Mediator.Register("SetPlatform", SetData);
            Mediator.Mediator.Register("GetPlatform", GetData);
        }

        ~PlatformViewModel()
        {
            Mediator.Mediator.Unregister("SetPlatform", SetData);
            Mediator.Mediator.Unregister("GetPlatform", GetData);
        }
        #endregion

        #region Private Variables
        private bool _DMD = false;
        private bool _D45 = false;
        private bool _FS = false;
        private bool _FI = false;
        private bool _BI = false;
        private bool _BU = false;
        private bool _FSBU = false;
        #endregion

        #region Public Variables
        public bool DMD
        {
            get { return _DMD; }
            set
            {
                _DMD = value;
                RisePropoertyChanged();
            }
        }
        public bool D45
        {
            get { return _D45; }
            set
            {
                _D45 = value;
                RisePropoertyChanged();
            }
        }
        public bool FS
        {
            get { return _FS; }
            set
            {
                _FS = value;
                RisePropoertyChanged();
            }
        }
        public bool FI
        {
            get { return _FI; }
            set
            {
                _FI = value;
                RisePropoertyChanged();
            }
        }
        public bool BI
        {
            get { return _BI; }
            set
            {
                _BI = value;
                RisePropoertyChanged();
            }
        }
        public bool BU
        {
            get { return _BU; }
            set
            {
                _BU = value;
                RisePropoertyChanged();
            }
        }
        public bool FSBU
        {
            get { return _FSBU; }
            set
            {
                _FSBU = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Mediator Function
        private void SetData(object obj)
        {
            var SetData = obj as PlatformModel;
            DMD = SetData.DMD;
            D45 = SetData.D45;
            FS = SetData.FS;
            FI = SetData.FI;
            BI = SetData.BI;
            FSBU = SetData.FSBU;
        }

        private void GetData(object SaveModel)
        {
            (SaveModel as PlatformModel).DMD = _DMD;
            (SaveModel as PlatformModel).D45 = _D45;
            (SaveModel as PlatformModel).FS = _FS;
            (SaveModel as PlatformModel).FI = _FI;
            (SaveModel as PlatformModel).BI = _BI;
            (SaveModel as PlatformModel).FSBU = _FSBU;

        }
        #endregion
    }
}
