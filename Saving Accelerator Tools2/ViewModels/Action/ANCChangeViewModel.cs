using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Views.Action;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class ANCChangeViewModel : INotifyProperty
    {
        public ANCChangeViewModel()
        {
            Calculate_Delta();
            PlusButton = new RelayCommand<object>(Plus_VisibeOneMoreANC);
            MinusButton = new RelayCommand<object>(Minus_VisibleOneLessANC);
        }

        private List<Visibility> _Visible = new List<Visibility> { Visibility.Visible, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden };
        private bool _VisibleSum = true;
        private List<string> _OldANC = EmptyList();
        private List<string> _NewANC = EmptyList();
        private List<string> _OldANCQuantity = EmptyList();
        private List<string> _NewANCQuantity = EmptyList();
        private List<string> _OldSTK = EmptyList();
        private List<string> _NewSTK = EmptyList();
        private List<string> _Delta = EmptyList();
        private List<string> _Estymation = EmptyList();
        private List<string> _Percent = ListWithThesameString("100");
        private List<string> _Calculation = EmptyList();

        private string _OldSum = string.Empty;
        private string _NewSum = string.Empty;
        private string _DeltaSum = string.Empty;
        private string _EstimationSum = string.Empty;
        private string _CalculationSum = string.Empty;

        public List<Visibility> Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
                RisePropoertyChanged();
            }
        }
        public bool VisibleSum
        {
            get { return _VisibleSum; }
            set
            {
                _VisibleSum = value;
                RisePropoertyChanged();
            }
        }
        public List<string> OldANC
        {
            get
            {
                UpdateSTK_Old();
                return _OldANC;
            }
            set
            {
                _OldANC = value;
                RisePropoertyChanged();
            }
        }
        public List<string> NewANC
        {
            get { return _NewANC; }
            set
            {
                _NewANC = value;
                RisePropoertyChanged();
            }
        }
        public List<string> OldANCQuantity
        {
            get { return _OldANCQuantity; }
            set
            {
                _OldANCQuantity = value;
                RisePropoertyChanged();
            }
        }
        public List<string> NewANCQuantity
        {
            get { return _NewANCQuantity; }
            set
            {
                _NewANCQuantity = value;
                RisePropoertyChanged();
            }
        }
        public List<string> OldSTK
        {
            get { return _OldSTK; }
            set
            {
                _OldSTK = value;
                SumOldAll();
                Calculate_Delta();
                RisePropoertyChanged();
            }
        }
        public List<string> NewSTK
        {
            get { return _NewSTK; }
            set
            {
                _NewSTK = value;
                SumNewAll();
                Calculate_Delta();
                RisePropoertyChanged();
            }
        }
        public List<string> Delta
        {
            get { return _Delta; }
            set
            {
                _Delta = value;
                SumDeltaAll();
                GeneretedCalculationValue();
                RisePropoertyChanged();
            }
        }
        public List<string> Estymation
        {
            get
            {
                SumEstymationAll();
                GeneretedCalculationValue();
                return _Estymation;
            }
            set
            {
                _Estymation = value;
                RisePropoertyChanged();
            }
        }
        public List<string> Percent
        {
            get
            {
                GeneretedCalculationValue();
                return _Percent;
            }
            set
            {
                _Percent = value;
                RisePropoertyChanged();
            }
        }
        public List<string> Calculation
        {
            get { return _Calculation; }
            set
            {
                _Calculation = value;
                SumCalcAll();
                RisePropoertyChanged();
            }
        }

        public string OldSum
        {
            get { return _OldSum; }
            set
            {
                _OldSum = value;
                RisePropoertyChanged();
            }
        }
        public string NewSum
        {
            get { return _NewSum; }
            set
            {
                _NewSum = value;
                RisePropoertyChanged();
            }
        }
        public string DeltaSum
        {
            get { return _DeltaSum; }
            set
            {
                _DeltaSum = value;
                RisePropoertyChanged();
            }
        }
        public string EstimationSum
        {
            get { return _EstimationSum; }
            set
            {
                _EstimationSum = value;
                RisePropoertyChanged();
            }
        }
        public string CalculationSum
        {
            get { return _CalculationSum; }
            set
            {
                _CalculationSum = value;
                RisePropoertyChanged();
            }
        }

        private static List<string> EmptyList()
        {
            var Lista = new List<string>();

            for (int counter = 1; counter <= 10; counter++)
            {
                Lista.Add(string.Empty);
            }
            return Lista;
        }
        private static List<string> ListWithThesameString(string value)
        {
            var Lista = new List<string>();

            for (int counter = 1; counter <= 10; counter++)
            {
                Lista.Add(value);
            }
            return Lista;
        }

        private void SumOldAll()
        {
            decimal Sum = 0;
            foreach (var one in _OldSTK)
            {
                if (decimal.TryParse(one, out decimal result))
                    Sum += result;
            }

            if (Sum == 0)
                OldSum = string.Empty;
            else
                OldSum = Sum.ToString();
        }
        private void SumNewAll()
        {
            decimal Sum = 0;
            foreach (var one in _NewSTK)
            {
                if (decimal.TryParse(one, out decimal result))
                    Sum += result;
            }

            if (Sum == 0)
                NewSum = string.Empty;
            else
                NewSum = Sum.ToString();
        }
        private void SumDeltaAll()
        {
            decimal Sum = 0;
            foreach (var one in _Delta)
            {
                if (decimal.TryParse(one, out decimal result))
                    Sum += result;
            }

            if (Sum == 0)
                DeltaSum = string.Empty;
            else
                DeltaSum = Sum.ToString();
        }
        private void SumCalcAll()
        {
            decimal Sum = 0;
            foreach (var one in _Calculation)
            {
                if (decimal.TryParse(one, out decimal result))
                    Sum += result;
            }

            if (Sum == 0)
                CalculationSum = string.Empty;
            else
                CalculationSum = Sum.ToString();
        }
        private void SumEstymationAll()
        {
            decimal Sum = 0;
            foreach (var one in _Estymation)
            {
                if (decimal.TryParse(one, out decimal result))
                    Sum += result;
            }

            if (Sum == 0)
                EstimationSum = string.Empty;
            else
                EstimationSum = Sum.ToString();
        }

        private void GeneretedCalculationValue()
        {
            for (int counter = 0; counter < 10; counter++)
            {
                if (_Estymation[counter] != string.Empty)
                {
                    if (decimal.TryParse(_Estymation[counter], out decimal EstymationValue))
                    {
                        if (decimal.TryParse(_Percent[counter], out decimal PercentValue))
                        {
                            _Calculation[counter] = Math.Round(EstymationValue * (PercentValue / 100), 4, MidpointRounding.AwayFromZero).ToString();
                        }
                    }
                }
                else if (_Delta[counter] != string.Empty)
                {
                    if (decimal.TryParse(_Delta[counter], out decimal DeltaValue))
                    {
                        if (decimal.TryParse(_Percent[counter], out decimal PercentValue))
                        {
                            _Calculation[counter] = Math.Round(DeltaValue * (PercentValue / 100), 4, MidpointRounding.AwayFromZero).ToString();
                        }
                    }
                }
                else
                {
                    _Calculation[counter] = string.Empty;
                }
            }
            Calculation = _Calculation;
        }
        private void Calculate_Delta()
        {
            for (int counter = 0; counter < 10; counter++)
            {
                if (_OldSTK[counter] != string.Empty || _NewSTK[counter] != string.Empty)
                {
                    decimal.TryParse(_OldSTK[counter], out decimal Old);
                    decimal.TryParse(_NewSTK[counter], out decimal New);

                    decimal Delta = Math.Round(Old - New, 4, MidpointRounding.AwayFromZero);
                    _Delta[counter] = Delta.ToString();
                }
                else if (_OldSTK[counter] == string.Empty && _NewSTK[counter] == string.Empty)
                {
                    _Delta[counter] = string.Empty;
                }
            }
            Delta = _Delta;
        }

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
            var visibleCount = _Visible.FindIndex(u => u == Visibility.Hidden);
            if (visibleCount != -1)
            {
                _Visible[visibleCount] = Visibility.Visible;
                Visible = _Visible;
            }
        }

        private void Minus_VisibleOneLessANC(object obj)
        {
            var visibleCount = _Visible.FindLastIndex(u => u == Visibility.Visible);
            if (visibleCount != -1)
            {
                _Visible[visibleCount] = Visibility.Hidden;
                Visible = Visible;

                _OldANC[visibleCount] = string.Empty;
                OldANC = _OldANC;

                _NewANC[visibleCount] = string.Empty;
                NewANC = _NewANC;

                _OldANCQuantity[visibleCount] = string.Empty;
                OldANCQuantity = _OldANCQuantity;

                _NewANCQuantity[visibleCount] = string.Empty;
                NewANCQuantity = _NewANCQuantity;

                _OldSTK[visibleCount] = string.Empty;
                OldSTK = _OldSTK;

                _NewSTK[visibleCount] = string.Empty;
                NewSTK = _NewSTK;

                _Estymation[visibleCount] = string.Empty;
                Estymation = _Estymation;

                _Percent[visibleCount] = "100";
                Percent = _Percent;

                if (visibleCount == 0)
                {
                    _Visible[visibleCount] = Visibility.Visible;
                    Visible = Visible;
                }
            }
        }

        public void SetVisibleANCRows(int VisibleRows)
        {
            for (int SetVisibleRow = 1; SetVisibleRow < VisibleRows; SetVisibleRow++)
            {

            }
        }

        private void UpdateSTK_Old()
        {
            OldSTK = StandradCost_Controller.UpdateAction(_OldANC, 2020);
        }
    }
}
