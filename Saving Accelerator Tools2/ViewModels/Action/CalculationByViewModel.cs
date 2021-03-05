using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Windows.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class CalculationByViewModel : INotifyProperty
    {
        #region Constructors
        public CalculationByViewModel()
        {
            Mediator.Mediator.Register("Set_Calc", Set_Value);
            Mediator.Mediator.Register("Get_Calc", Get_Value);

            PNCQuantity = new RelayCommand<object>(AddPNC);
            PNCSQuantity = new RelayCommand<object>(AddPNCSpecial);
        }

        ~CalculationByViewModel()
        {
            Mediator.Mediator.Unregister("Set_Calc", Set_Value);
            Mediator.Mediator.Unregister("Get_Calc", Get_Value);
        }
        #endregion

        #region Private Variables
        private bool _ANC = true;
        private bool _ANCS;
        private bool _PNC;
        private bool _PNCS;
        #endregion

        #region Public Variables
        public bool ANC
        {
            get { return _ANC; }
            set
            {
                _ANC = value;
                if (value)
                {
                    Mediator.Mediator.NotifyColleagues("PNC_Visibilit", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("ANCSpecial_Visibility", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("Set_Visibility_PNCSpecial", Visibility.Hidden);
                }
                RisePropoertyChanged();
            }
        }
        public bool ANCSpecial
        {
            get { return _ANCS; }
            set
            {
                _ANCS = value;
                if (value)
                {
                    Mediator.Mediator.NotifyColleagues("PNC_Visibilit", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("ANCSpecial_Visibility", Visibility.Visible);
                    Mediator.Mediator.NotifyColleagues("Set_Visibility_PNCSpecial", Visibility.Hidden);
                }
                RisePropoertyChanged();
            }
        }
        public bool PNC
        {
            get { return _PNC; }
            set
            {
                _PNC = value;
                if (value)
                {
                    Mediator.Mediator.NotifyColleagues("PNC_Visibilit", Visibility.Visible);
                    Mediator.Mediator.NotifyColleagues("ANCSpecial_Visibility", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("Set_Visibility_PNCSpecial", Visibility.Hidden);
                }
                RisePropoertyChanged();
            }
        }
        public bool PNCSpecial
        {
            get { return _PNCS; }
            set
            {
                _PNCS = value;
                if (value)
                {
                    Mediator.Mediator.NotifyColleagues("PNC_Visibilit", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("ANCSpecial_Visibility", Visibility.Hidden);
                    Mediator.Mediator.NotifyColleagues("Set_Visibility_PNCSpecial", Visibility.Visible);
                }
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand PNCQuantity
        {
            get; private set;
        }
        public ICommand PNCSQuantity
        {
            get; private set;
        }

        private void AddPNC(object obj)
        {
            var AddDialog = new AddingData("PNC_ActionList");
            AddDialog.Show();
        }
        private void AddPNCSpecial(object obj)
        {
            var PNCSpecialWindow = new PNCSpecialNewData();
            PNCSpecialWindow.Show();
        }
        #endregion

        #region Mediators Function
        private void Set_Value(object NewValue)
        {
            switch ((int)NewValue)
            {
                case 1: ANC = true; break;
                case 2: ANCSpecial = true; break;
                case 3: PNC = true; break;
                case 4: PNCSpecial = true; break;
                default: ANC = true; break;
            }

        }
        private void Get_Value(object obj)
        {
            int grup = 0;
            if (_ANC)
                grup = 1;
            else if (_ANCS)
                grup = 2;
            else if (_PNC)
                grup = 3;
            else if (_PNCS)
                grup = 4;

            Mediator.Mediator.NotifyColleagues("", grup);
        }
        #endregion
    }
}
