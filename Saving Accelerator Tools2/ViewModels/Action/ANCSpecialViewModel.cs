using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Mediator.Mediator.Register("Set_ANCSpecial_PLusMinus", Load_Items);
            Mediator.Mediator.Register("Get_ANCSpecial_Platform", Save_Platform);
            Mediator.Mediator.Register("Get_ANCSpecial_PLusMinus", Save_Items);
            Mediator.Mediator.Register("ANCSpecial_Platform", PlatformSave);
            Mediator.Mediator.Register("ANCSpecial_PlusMinus", PlusMinusSave);
        }
        ~ANCSpecialViewModel()
        {
            Mediator.Mediator.Unregister("ANCSpecial_Visibility", Set_Visibility);
            Mediator.Mediator.Unregister("Set_ANCSpecial_Platform", Load_Platform);
            Mediator.Mediator.Unregister("Set_ANCSpecial_PLusMinus", Load_Items);
            Mediator.Mediator.Unregister("Get_ANCSpecial_Platform", Save_Platform);
            Mediator.Mediator.Unregister("Get_ANCSpecial_PLusMinus", Save_Items);
            Mediator.Mediator.Unregister("ANCSpecial_Platform", PlatformSave);
            Mediator.Mediator.Unregister("ANCSpecial_PlusMinus", PlusMinusSave);
        }
        #endregion

        #region Privates Variables
        private Visibility _UserControlEnabled = Visibility.Hidden;
        private List<PlusMinusModel> Items = new List<PlusMinusModel>();
        private List<ANCSpecialModels> Platforms = new List<ANCSpecialModels>();
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
                else if(_PlusMinus == false)
                {
                    PlusMinu = true;
                    Checkbox_Clear();
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
                else if (_Platform == false)
                {
                    Platform = true;
                    PlusMinus_Clear();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
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
                _ = new Calculation_ActionView();
            }
        }
        public string Minus
        {
            get { return _Minus; }
            set
            {
                _Minus = value;
                RisePropoertyChanged();
                _ = new Calculation_ActionView();
            }
        }
        #endregion

        #region Functions
        private void AllCheck(bool Value)
        {
            if (Value)
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
            if (Value)
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
            if (_DMD_FS && _DMD_FI && _DMD_BI && _DMD_FSBU)
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
            if (_DMD && _D45)
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
            Platforms = NewData as List<ANCSpecialModels>;
            if (Platforms.Count > 0)
            {
                Platform = true;
                if (Platforms.Any(b => b.Platform == "All"))
                {
                    All = true;
                }
                else
                {
                    All = false;
                    if (Platforms.Any(b => b.Platform == "DMD" && b.Installation == string.Empty))
                    {
                        DMD = true;
                    }
                    else
                    {
                        DMD = false;
                        _ = Platforms.Any(b => b.Platform == "DMD" && b.Installation == "FS") ? DMD_FS = true : DMD_FS = false;
                        _ = Platforms.Any(b => b.Platform == "DMD" && b.Installation == "FI") ? DMD_FI = true : DMD_FI = false;
                        _ = Platforms.Any(b => b.Platform == "DMD" && b.Installation == "BI") ? DMD_BI = true : DMD_BI = false;
                        _ = Platforms.Any(b => b.Platform == "DMD" && b.Installation == "FSBU") ? DMD_FSBU = true : DMD_FSBU = false;
                    }
                    if (Platforms.Any(b => b.Platform == "D45" && b.Installation == string.Empty))
                    {
                        D45 = true;
                    }
                    else
                    {
                        D45 = false;
                        _ = Platforms.Any(b => b.Platform == "D45" && b.Installation == "FS") ? D45_FS = true : D45_FS = false;
                        _ = Platforms.Any(b => b.Platform == "D45" && b.Installation == "FI") ? D45_FI = true : D45_FI = false;
                        _ = Platforms.Any(b => b.Platform == "D45" && b.Installation == "BI") ? D45_BI = true : D45_BI = false;
                        _ = Platforms.Any(b => b.Platform == "D45" && b.Installation == "FSBU") ? D45_FSBU = true : D45_FSBU = false;
                    }
                }
            }
        }
        private void Load_Items(object BaseItems)
        {
            Items.Clear();
            string NewPlus = string.Empty;
            string NewMinus = string.Empty;

            if ((BaseItems as List<PlusMinusModel>).Count > 0)
            {
                PlusMinu = true;
                foreach (var NewItem in BaseItems as List<PlusMinusModel>)
                {
                    Items.Add(NewItem);
                    _ = NewItem.Plus ? NewPlus += NewItem.Item + Environment.NewLine : NewMinus += NewItem.Item + Environment.NewLine;
                }
                Plus = NewPlus;
                Minus = NewMinus;
            }
        }
        private void Save_Platform(object obj)
        {
            List<int> NewTable = new List<int>();
            if(_All)
            {
                NewTable.Add(1);
            }
            else
            {
                if(_DMD)
                {
                    NewTable.Add(3);
                }
                else
                {
                    if (_DMD_FS)
                        NewTable.Add(4);
                    if (_DMD_FI)
                        NewTable.Add(5);
                    if (_DMD_BI)
                        NewTable.Add(6);
                    if (_DMD_FSBU)
                        NewTable.Add(7);
                }
                if(_D45)
                {
                    NewTable.Add(2);
                }
                else
                {
                    if (_D45_FS)
                        NewTable.Add(8);
                    if (_D45_FI)
                        NewTable.Add(9);
                    if (_D45_BI)
                        NewTable.Add(10);
                    if (_D45_FSBU)
                        NewTable.Add(11);
                }
            }
            Mediator.Mediator.NotifyColleagues("ANCSpecial_Platform", NewTable);
        }
        private void Save_Items(object obj)
        {
            string[] PlusValue = _Plus?.Split(Environment.NewLine);
            string[] MinusValue = _Minus?.Split(Environment.NewLine);

            if (PlusValue != null)
            {
                foreach (var PlusRecord in PlusValue)
                {
                    if (!Items.Any(b => b.Item == PlusRecord && b.Plus == true))
                    {
                        var NewRecord = new PlusMinusModel()
                        {
                            ID = 0,
                            Item = PlusRecord,
                            Plus = true,
                            Minus = false,
                            Active = true,
                        };
                        Items.Add(NewRecord);
                    }
                    else
                    {
                        var Item = Items.Where(b => b.Item == PlusRecord && b.Plus == true).FirstOrDefault();
                        Item.Active = true;
                    }
                }
            }

            if (Minus != null)
            {
                foreach (var MinusRecord in MinusValue)
                {
                    if (!Items.Any(b => b.Item == MinusRecord && b.Minus == true))
                    {
                        var newRecord = new PlusMinusModel()
                        {
                            ID = 0,
                            Item = MinusRecord,
                            Plus = false,
                            Minus = true,
                            Active = true,
                        };
                        Items.Add(newRecord);
                    }
                    else
                    {
                        var Item = Items.Where(b => b.Item == MinusRecord && b.Minus == true).FirstOrDefault();
                        Item.Active = true;
                    }
                }
            }
            Mediator.Mediator.NotifyColleagues("ANCChange_Items", Items);
        }
        private void PlatformSave (object PlatformData)
        {
            if (_All)
            {
                (PlatformData as List<int>).Add(1);
            }
            else
            {
                if (_DMD)
                {
                    (PlatformData as List<int>).Add(3);
                }
                else
                {
                    if (_DMD_FS)
                        (PlatformData as List<int>).Add(4);
                    if (_DMD_FI)
                        (PlatformData as List<int>).Add(5);
                    if (_DMD_BI)
                        (PlatformData as List<int>).Add(6);
                    if (_DMD_FSBU)
                        (PlatformData as List<int>).Add(7);
                }
                if (_D45)
                {
                    (PlatformData as List<int>).Add(2);
                }
                else
                {
                    if (_D45_FS)
                        (PlatformData as List<int>).Add(8);
                    if (_D45_FI)
                        (PlatformData as List<int>).Add(9);
                    if (_D45_BI)
                        (PlatformData as List<int>).Add(10);
                    if (_D45_FSBU)
                        (PlatformData as List<int>).Add(11);
                }
            }
        }
        private void PlusMinusSave(object Data)
        {
            string[] PlusValue = _Plus?.Split(Environment.NewLine);
            string[] MinusValue = _Minus?.Split(Environment.NewLine);

            if (PlusValue != null)
            {
                foreach (var PlusRecord in PlusValue)
                {
                    if (PlusRecord != string.Empty && PlusRecord.Length == 9)
                    {
                        var NewRecord = new PlusMinusModel()
                        {
                            ID = 0,
                            Item = PlusRecord,
                            Plus = true,
                            Minus = false,
                            Active = true,
                        };
                        (Data as List<PlusMinusModel>).Add(NewRecord);
                    }
                }
            }

            if (Minus != null)
            {
                foreach (var MinusRecord in MinusValue)
                {
                    if (MinusRecord != string.Empty && MinusRecord.Length == 9)
                    {
                        var newRecord = new PlusMinusModel()
                        {
                            ID = 0,
                            Item = MinusRecord,
                            Plus = false,
                            Minus = true,
                            Active = true,
                        };
                        (Data as List<PlusMinusModel>).Add(newRecord);
                    }
                }
            }
        }
        #endregion
    }
}
