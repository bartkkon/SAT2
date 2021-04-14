using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Views.Action;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class ANCChangeViewModel : INotifyProperty
    {
        #region Constructors
        public ANCChangeViewModel()
        {
            Mediator.Mediator.Register("Set_NewYear", SetYear);
            Mediator.Mediator.Register("Set_ANCChange", SetNewList);
            Mediator.Mediator.Register("Get_ANCChange", GetList);
            Mediator.Mediator.Register("Set_SumVisible", SetSumVisible);
            Mediator.Mediator.Register("CalcSum", CalculationSumAllValue);
            Mediator.Mediator.Register("Get_DetltaSum", DeltaSumANC);
            Mediator.Mediator.Register("Get_ANCChange_Object", GetObject);


            //Obsługa przycisków
            PlusButton = new RelayCommand<object>(Plus_VisibeOneMoreANC);
            MinusButton = new RelayCommand<object>(Minus_VisibleOneLessANC);

            ANCList.Add(new ANCChangeModel());
        }
        ~ANCChangeViewModel()
        {
            Mediator.Mediator.Unregister("Set_NewYear", SetYear);
            Mediator.Mediator.Unregister("Set_ANCChange", SetNewList);
            Mediator.Mediator.Unregister("Get_ANCChange", GetList);
            Mediator.Mediator.Unregister("Set_SumVisible", SetSumVisible);
            Mediator.Mediator.Unregister("CalcSum", CalculationSumAllValue); 
            Mediator.Mediator.Unregister("Get_DetltaSum", DeltaSumANC);
            Mediator.Mediator.Unregister("Get_ANCChange_Object", GetObject);
        }
        #endregion

        #region Privates Variables
        private ObservableCollection<ANCChangeModel> _ANCList = new ObservableCollection<ANCChangeModel>();

        private List<Visibility> _Visible = new List<Visibility> { Visibility.Visible, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden };
        private Visibility _SumVisible = Visibility.Visible;

        private decimal _Year;
        private decimal _OldSum;
        private decimal _NewSum;
        private decimal _DeltaSum;
        private decimal _EstimationSum;
        private decimal _CalculationSum;
        #endregion

        #region Public Variables
        public ObservableCollection<ANCChangeModel> ANCList
        {
            get { return _ANCList; }
            set
            {
                _ANCList = value;
                RisePropoertyChanged();
            }
        }

        public List<Visibility> Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
                CalculationSumAllValue(null);
                RisePropoertyChanged();
            }
        }
        public Visibility SumVisible
        {
            get { return _SumVisible; }
            set
            {
                _SumVisible = value;
                RisePropoertyChanged();
            }
        }

        public decimal OldSum
        {
            get { return _OldSum; }
            set
            {
                _OldSum = value;
                RisePropoertyChanged();
            }
        }
        public decimal NewSum
        {
            get { return _NewSum; }
            set
            {
                _NewSum = value;
                RisePropoertyChanged();
            }
        }
        public decimal DeltaSum
        {
            get { return _DeltaSum; }
            set
            {
                _DeltaSum = value;
                RisePropoertyChanged();
            }
        }
        public decimal EstimationSum
        {
            get { return _EstimationSum; }
            set
            {
                _EstimationSum = value;
                RisePropoertyChanged();
            }
        }
        public decimal CalculationSum
        {
            get { return _CalculationSum; }
            set
            {
                _CalculationSum = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand PlusButton
        {
            get; private set;
        }
        public ICommand MinusButton
        {
            get; private set;
        }

        private void Plus_VisibeOneMoreANC(object obj)
        {
            if (ANCList.Count < 10)
            {
                if (_Year == 0)
                {
                    do
                    {
                        Mediator.Mediator.NotifyColleagues("Get_Year", null);
                        Thread.Sleep(500);
                    }
                    while (_Year == 0);
                }
                _ANCList.Add(new ANCChangeModel() { Year = _Year });
                ANCList = _ANCList;
                _Visible[ANCList.Count - 1] = Visibility.Visible;
                Visible = _Visible;
            }
        }
        private void Minus_VisibleOneLessANC(object obj)
        {
            if (ANCList.Count > 0)
            {
                _ANCList.Remove(ANCList.Last());
                ANCList = _ANCList;
                _Visible[ANCList.Count] = Visibility.Hidden;
                Visible = _Visible;
            }
            if (ANCList.Count == 0)
            {
                if (_Year == 0)
                {
                    do
                    {
                        Mediator.Mediator.NotifyColleagues("Get_Year", null);
                        Thread.Sleep(500);
                    }
                    while (_Year == 0);
                }
                _ANCList.Add(new ANCChangeModel() { Year = _Year });
                ANCList = _ANCList;
                _Visible[0] = Visibility.Visible;
                Visible = _Visible;
            }
        }
        #endregion

        #region Mediators Function
        private void SetYear(object obj)
        {
            var NewYear = (decimal)obj;
            if (NewYear != _Year)
            {
                _Year = NewYear;
                foreach (var oneChange in ANCList)
                {
                    oneChange.Year = NewYear;
                }
            }
        }
        private void SetNewList(object obj)
        {
            ANCList.Clear();
            foreach(var Row in (obj as List<ANCChangeModel>))
            {
                ANCList.Add(Row);
            }

            for (int counter = 0; counter < ANCList.Count(); counter++)
            {
                _Visible[counter] = Visibility.Visible;
            }
            for (int counter = ANCList.Count(); counter < 10; counter++)
            {
                _Visible[counter] = Visibility.Hidden;
            }
            Visible = _Visible;
        }
        private void GetList(object obj)
        {
            List<ANCChangeModel> FinalList = new List<ANCChangeModel>();
            foreach (var OneRow in ANCList)
            {
                if (!string.IsNullOrEmpty(OneRow.OldANC) && !string.IsNullOrEmpty(OneRow.NewANC))
                {
                    FinalList.Add(OneRow);
                }
            }

            Mediator.Mediator.NotifyColleagues("Set_ANCList", FinalList);
        }
        private void SetSumVisible(object obj)
        {
            if((bool)obj)
            {
                SumVisible = Visibility.Visible;
            }
            else
            {
                SumVisible = Visibility.Hidden;
            }
        }
        private void CalculationSumAllValue(object obj)
        {
            _OldSum = 0;
            _NewSum = 0;
            _DeltaSum = 0;
            EstimationSum = 0;
            _CalculationSum = 0;
            foreach(var ANC in _ANCList)
            {
                _OldSum += ANC.OldSTK;
                _NewSum += ANC.NewSTK;
                _DeltaSum += ANC.Delta;
                _EstimationSum += ANC.UserEstymacja;
                _CalculationSum += ANC.Estymacja;
            }

            OldSum = _OldSum;
            NewSum = _NewSum;
            DeltaSum = _DeltaSum;
            EstimationSum = _EstimationSum;
            CalculationSum = _CalculationSum;
        }
        private void DeltaSumANC(object obj)
        {
            (obj as List<decimal>).Add(_DeltaSum);
            (obj as List<decimal>).Add(_CalculationSum);
        }
        private void GetObject(object Data)
        {
            foreach (var OneRow in ANCList)
            {
                if (!string.IsNullOrEmpty(OneRow.OldANC) && !string.IsNullOrEmpty(OneRow.NewANC))
                {
                    (Data as List<ANCChangeModel>).Add(OneRow);
                }
            }
        }
        #endregion
    }
}
