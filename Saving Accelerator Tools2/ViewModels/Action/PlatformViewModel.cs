using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class PlatformViewModel : INotifyProperty
    {
        public PlatformViewModel() { }

        private bool _DMD = false;
        private bool _D45 = false;
        private bool _FS = false;
        private bool _FI = false;
        private bool _BI = false;
        private bool _BU = false;
        private bool _FSBU = false;

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
    }
}
