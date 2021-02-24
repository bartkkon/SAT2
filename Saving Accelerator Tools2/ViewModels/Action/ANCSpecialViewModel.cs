using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class ANCSpecialViewModel : INotifyProperty
    {
        #region Constructors
        public ANCSpecialViewModel()
        {
            Mediator.Mediator.Register("ANCSpecial_Visibility", Set_Visibility);
            Mediator.Mediator.Register("Set_ANCSpecial_Platform", Load_Platform);
        }
        ~ANCSpecialViewModel()
        {
            Mediator.Mediator.Unregister("ANCSpecial_Visibility", Set_Visibility);
            Mediator.Mediator.Unregister("Set_ANCSpecial_Platform", Load_Platform);
        }
        #endregion

        #region Privates Variables
        private Visibility _UserControlEnabled = Visibility.Hidden;
        private bool _Platform = true;
        private bool _PlusMinus = false;
        private bool _All;
        private bool _DMD;
        private bool _D45;
        private bool _DMD_FS;
        private bool _DMD_FI;
        private bool _DMD_BI;
        private bool _DMD_FSBU;
        private bool _D45_FS;
        private bool _D45_FI;
        private bool _D45_BI;
        private bool _D45_FSBU;
        private bool _DMD_Enabled = true;
        private bool _D45_Enabled = true;
        private bool _DMD_In_Enabled = true;
        private bool _D45_In_Enabled = true;

        private string _Plus;
        private string _Minus;
        #endregion

        #region Public Variables
        public Visibility UserControlEnabled
        {
            get { return _UserControlEnabled; }
            set
            {
                _UserControlEnabled = value;
                RisePropoertyChanged();
            }
        }
        public bool Platform
        {
            get { return _Platform; }
            set
            {
                _Platform = value;
                if (value == true)
                {
                    PlusMinu = false;
                    PlusMinus_Clear();
                }
                RisePropoertyChanged();
            }
        }
        public bool PlusMinu
        {
            get { return _PlusMinus; }
            set
            {
                _PlusMinus = value;
                if (value == true)
                {
                    Platform = false;
                    Checkbox_Clear();
                }
                RisePropoertyChanged();
            }
        }
        public bool All
        {
            get { return _All; }
            set
            {
                _All = value;
                AllCheck(value);
                RisePropoertyChanged();
            }
        }
        public bool DMD
        {
            get { return _DMD; }
            set
            {
                _DMD = value;
                StructureCheck_DMD(value);
                PlatformCheckAll();
                RisePropoertyChanged();
            }
        }
        public bool D45
        {
            get { return _D45; }
            set
            {
                _D45 = value;
                StructureCheck_D45(value);
                PlatformCheckAll();
                RisePropoertyChanged();
            }
        }
        public bool DMD_FS
        {
            get { return _DMD_FS; }
            set
            {
                _DMD_FS = value;
                DMD_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool DMD_FI
        {
            get { return _DMD_FI; }
            set
            {
                _DMD_FI = value;
                DMD_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool DMD_BI
        {
            get { return _DMD_BI; }
            set
            {
                _DMD_BI = value;
                DMD_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool DMD_FSBU
        {
            get { return _DMD_FSBU; }
            set
            {
                _DMD_FSBU = value;
                DMD_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool D45_FS
        {
            get { return _D45_FS; }
            set
            {
                _D45_FS = value;
                D45_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool D45_FI
        {
            get { return _D45_FI; }
            set
            {
                _D45_FI = value;
                D45_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool D45_BI
        {
            get { return _D45_BI; }
            set
            {
                _D45_BI = value;
                D45_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool D45_FSBU
        {
            get { return _D45_FSBU; }
            set
            {
                _D45_FSBU = value;
                D45_CheckIfAll();
                RisePropoertyChanged();
            }
        }
        public bool DMD_Enabled
        {
            get { return _DMD_Enabled; }
            set
            {
                _DMD_Enabled = value;
                RisePropoertyChanged();
            }
        }
        public bool D45_Enabled
        {
            get { return _D45_Enabled; }
            set
            {
                _D45_Enabled = value;
                RisePropoertyChanged();
            }
        }
        public bool DMD_Ins_Enabled
        {
            get { return _DMD_In_Enabled; }
            set
            {
                _DMD_In_Enabled = value;
                RisePropoertyChanged();
            }
        }
        public bool D45_Ins_Enabled
        {
            get { return _D45_In_Enabled; }
            set
            {
                _D45_In_Enabled = value;
                RisePropoertyChanged();
            }
        }

        public string Plus
        {
            get { return _Plus; }
            set
            {
                _Plus = value;
                RisePropoertyChanged();
            }
        }
        public string Minus
        {
            get { return _Minus; }
            set
            {
                _Minus = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Functions
        private void AllCheck(bool Value)
        {
            if(Value)
            {
                DMD = false;
                D45 = false;
                DMD_FS = false;
                DMD_FI = false;
                DMD_BI = false;
                DMD_FSBU = false;
                D45_FS = false;
                D45_FI = false;
                D45_BI = false;
                D45_FSBU = false;
                DMD_Enabled = false;
                D45_Enabled = false;
                DMD_Ins_Enabled = false;
                D45_Ins_Enabled = false;
            }
            else
            {
                DMD_Enabled = true;
                D45_Enabled = true;
                DMD_Ins_Enabled = true;
                D45_Ins_Enabled = true;
            }
        }
        private void StructureCheck_DMD(bool Value)
        {
            if(Value)
            {
                DMD_FS = false;
                DMD_FI = false;
                DMD_BI = false;
                DMD_FSBU = false;
                DMD_Ins_Enabled = false;
            }
            else
            {
                DMD_Ins_Enabled = true;
            }
        }
        private void StructureCheck_D45(bool Value)
        {
            if (Value)
            {
                D45_FS = false;
                D45_FI = false;
                D45_BI = false;
                D45_FSBU = false;
                D45_Ins_Enabled = false;
            }
            else
            {
                D45_Ins_Enabled = true;
            }
        }
        private void DMD_CheckIfAll()
        {
            if(_DMD_FS && _DMD_FI && _DMD_BI && _DMD_FSBU)
            {
                DMD = true;
            }
        }
        private void D45_CheckIfAll()
        {
            if (_D45_FS && _D45_FI && _D45_BI && _D45_FSBU)
            {
                D45 = true;
            }
        }
        private void PlatformCheckAll()
        {
            if(_DMD &&_D45)
            {
                All = true;
            }
        }
        private void Checkbox_Clear()
        {
            All = false;
            DMD = false;
            D45 = false;
            DMD_FS = false;
            DMD_FI = false;
            DMD_BI = false;
            DMD_FSBU = false;
            D45_FS = false;
            D45_FI = false;
            D45_BI = false;
            D45_FSBU = false;
        }
        private void PlusMinus_Clear()
        {
            Plus = string.Empty;
            Minus = string.Empty;
        }
        #endregion

        #region Mediators
        private void Set_Visibility(object NewEnabledValue)
        {
            UserControlEnabled = (Visibility)NewEnabledValue;
        }
        private void Load_Platform(object NewData)
        {
            List<PlatformCalc_DB> Platform = NewData as List<PlatformCalc_DB>;
            if(Platform.Count >0)
            {
                if( Platform.Any(b => b.Platform == "All"))
                {
                    All = true;
                }
                else
                {
                    All = false;
                    if(Platform.Any(b => b.Platform == "DMD" && b.Installation == string.Empty))
                    {
                        DMD = true;
                    }
                    else
                    {
                        DMD = false;
                        _ = Platform.Any(b => b.Platform == "DMD" && b.Installation == "FS") ? DMD_FS = true : DMD_FS = false;
                        _ = Platform.Any(b => b.Platform == "DMD" && b.Installation == "FI") ? DMD_FI = true : DMD_FI = false;
                        _ = Platform.Any(b => b.Platform == "DMD" && b.Installation == "BI") ? DMD_BI = true : DMD_BI = false;
                        _ = Platform.Any(b => b.Platform == "DMD" && b.Installation == "FSBU") ? DMD_FSBU = true : DMD_FSBU = false;
                    }
                    if (Platform.Any(b => b.Platform == "D45" && b.Installation == string.Empty))
                    {
                        D45 = true;
                    }
                    else
                    {
                        D45 = false;
                        _ = Platform.Any(b => b.Platform == "D45" && b.Installation == "FS") ? D45_FS = true : D45_FS = false;
                        _ = Platform.Any(b => b.Platform == "D45" && b.Installation == "FI") ? D45_FI = true : D45_FI = false;
                        _ = Platform.Any(b => b.Platform == "D45" && b.Installation == "BI") ? D45_BI = true : D45_BI = false;
                        _ = Platform.Any(b => b.Platform == "D45" && b.Installation == "FSBU") ? D45_FSBU = true : D45_FSBU = false;
                    }
                }
            }
        }
        private void Load_PlusMinus(object NewData)
        {

        }
        #endregion
    }
}
