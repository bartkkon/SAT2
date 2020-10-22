using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class ANCChangeViewModel : INotifyProperty
    {
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
        private List<string> _Percent = EmptyList();
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
            get { return _VisibleSum;   }
            set
            {
                _VisibleSum = value;
                RisePropoertyChanged();
            }
        }
        public List<string> OldANC
        {
            get { return _OldANC; }
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
                RisePropoertyChanged();
            }
        }
        public List<string> NewSTK
        {
            get { return _NewSTK; }
            set
            {
                _NewSTK = value;
                RisePropoertyChanged();
            }
        }
        public List<string> Delta
        {
            get { return _Delta; }
            set
            {
                _Delta = value;
                RisePropoertyChanged();
            }
        }
        public List<string> Estymation
        {
            get { return _Estymation; }
            set
            {
                _Estymation = value;
                RisePropoertyChanged();
            }
        }
        public List<string> Percent
        {
            get { return _Percent; }
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

    }
}
