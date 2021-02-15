using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class ANCChangeModel : INotifyProperty
    {
        #region Provate Variables
        private decimal _Year = DateTime.UtcNow.Year;

        private int _ID = 0;
        private string _OldANC;
        private decimal _OldANCQuantity;
        private decimal _OldSTK;
        private string _NewANC;
        private decimal _NewANCQuantity;
        private decimal _NewSTK;
        private decimal _Delta;
        private decimal _UserEstymacja;
        private decimal _Percent = 100;
        private decimal _Estymacja;
        private string _NextANC1;
        private string _NextANC2;
        #endregion

        #region Public Variables
        public decimal Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                FindSTK(true);
                FindSTK(false);
                RisePropoertyChanged();
            }
        }
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                RisePropoertyChanged();
            }
        }
        public string OldANC
        {
            get { return _OldANC; }
            set
            {
                _OldANC = value;
                
                if (_OldANC != string.Empty)
                {
                    if (_OldANCQuantity == 0)
                    {
                        OldANCQuantity = 1;
                    }
                    FindSTK(true);
                }
                else
                {
                    OldANCQuantity = 0;
                    OldSTK = 0;
                }
                RisePropoertyChanged();
            }
        }
        public decimal OldANCQuantity
        {
            get { return _OldANCQuantity; }
            set
            {
                _OldANCQuantity = value;
                FindSTK(true);
                RisePropoertyChanged();
            }
        }
        public decimal OldSTK
        {
            get { return _OldSTK; }
            set
            {
                _OldSTK = value;
                CalcDelta();
                RisePropoertyChanged();
            }
        }
        public string NewANC
        {
            get { return _NewANC; }
            set
            {
                _NewANC = value;
                if (_NewANC != string.Empty)
                {
                    if(_NewANCQuantity ==0)
                    {
                        NewANCQuantity = 1;
                    }
                    FindSTK(false);
                }
                else
                {
                    NewANCQuantity = 0;
                    NewSTK = 0;
                }
                RisePropoertyChanged();
            }
        }
        public decimal NewANCQuantity
        {
            get { return _NewANCQuantity; }
            set
            {
                _NewANCQuantity = value;
                FindSTK(false);
                RisePropoertyChanged();
            }
        }
        public decimal NewSTK
        {
            get { return _NewSTK; }
            set
            {
                _NewSTK = value;
                CalcDelta();
                RisePropoertyChanged();
            }
        }
        public decimal Delta
        {
            get { return _Delta; }
            set
            {
                _Delta = value;
                CalcEstymation();
                RisePropoertyChanged();
            }
        }
        public decimal UserEstymacja
        {
            get { return _UserEstymacja; }
            set
            {
                _UserEstymacja = value;
                CalcEstymation();
                RisePropoertyChanged();
            }
        }
        public decimal Percent
        {
            get { return _Percent; }
            set
            {
                _Percent = value;
                CalcEstymation();
                RisePropoertyChanged();
            }
        }
        public decimal Estymacja
        {
            get { return _Estymacja; }
            set
            {
                _Estymacja = value;
                Mediator.Mediator.NotifyColleagues("CalcSum", null);
                RisePropoertyChanged();
            }
        }
        public string NextANC1
        {
            get { return _NextANC1; }
            set
            {
                _NextANC1 = value;
                RisePropoertyChanged();
            }
        }
        public string NextANC2
        {
            get { return _NextANC2; }
            set
            {
                _NextANC2 = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Provates Function
        private void FindSTK(bool OldCalc)
        {// true when calculate for OldANc -> false when for New ANC
            if (OldCalc)
            {
                var STK = STK_Controller.FindItem(_OldANC, _Year);
                if (STK != null)
                {
                    OldSTK = Math.Round(STK.STD * _OldANCQuantity, 4, MidpointRounding.AwayFromZero);
                }
                else
                {
                    OldSTK = 0;
                }
            }
            else
            {
                var STK = STK_Controller.FindItem(_NewANC, _Year);
                if (STK != null)
                {
                    NewSTK = Math.Round(STK.STD * _NewANCQuantity, 4, MidpointRounding.AwayFromZero);
                }
                else
                {
                    NewSTK = 0;
                }
            }
        }
        private void CalcDelta()
        {
            Delta = _OldSTK - _NewSTK;
        }
        private void CalcEstymation()
        {
            if (UserEstymacja != 0)
            {
                Estymacja = Math.Round(_UserEstymacja * (_Percent / 100), 4, MidpointRounding.AwayFromZero);
            }
            else
            {
                Estymacja = Math.Round(_Delta * (_Percent / 100), 4, MidpointRounding.AwayFromZero);
            }
        }
        #endregion
    }
}
