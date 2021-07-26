using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Action.Sub;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Action;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class ANCChangeViewModel : Base
    {
        private ActionBase loadedAction;
        private readonly IStandardCostServices STKServices;

        public ANCChangeViewModel(ActionBase LoadedAction, IStandardCostServices STKServices)
        {
            PlusButton = new RelayCommand(Plus);
            MinusButton = new RelayCommand(Minus);



            loadedAction = LoadedAction;
            this.STKServices = STKServices;
            loadedAction.ANCChanges = new ObservableCollection<ANCChange>();

            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
            loadedAction.ANCChanges.CollectionChanged += ANCChanges_CollectionChanged;

            loadedAction.ANCChanges.Add(new() { NewANC = "A05912217", Delta = 5, OldANC_STD = 23.35m, NewANC_STD = 36.598m });
            loadedAction.ANCChanges.Add(new() { Delta = -6 });
            loadedAction.ANCChanges.Add(new() { Delta = 0.0015m });
            loadedAction.ANCChanges.Add(new() { Delta = 0.268m });
        }

        private void ANCChanges_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged oldItem in e.OldItems)
                {
                    oldItem.PropertyChanged -= Items_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged newItem in e.NewItems)
                {
                    newItem.PropertyChanged += Items_PropertyChanged;
                }
            }
        }

        private decimal SumCalculation(string ANCChangeNameOf)
        {
            decimal result = 0;
            foreach (var Row in loadedAction.ANCChanges)
            {
                result += (decimal)typeof(ANCChange).GetProperty(ANCChangeNameOf).GetValue(Row, null);
            }

            return result;
        }

        private void Items_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ANCChange.Estymation))
            {
                EstymationSum = SumCalculation(nameof(ANCChange.Estymation));
            }
            if (e.PropertyName == nameof(ANCChange.OldANC_STD))
            {
                OldSTKSum = SumCalculation(nameof(ANCChange.OldANC_STD));
            }
            if (e.PropertyName == nameof(ANCChange.NewANC_STD))
            {
                NewSTKSum = SumCalculation(nameof(ANCChange.NewANC_STD));
            }
            if (e.PropertyName == nameof(ANCChange.Delta))
            {
                DeltaSum = SumCalculation(nameof(ANCChange.Delta));
            }
            if (e.PropertyName == nameof(ANCChange.FinalCalc))
            {
                EstymationSum = SumCalculation(nameof(ANCChange.FinalCalc));
            }
            if(e.PropertyName is nameof(ANCChange.NewANC) or nameof(ANCChange.OldANC) or nameof(ANCChange.NewANC_Q) or nameof(ANCChange.OldANC_Q))
            {
                ANCChange ChangeRow = sender as ANCChange;
                if(!string.IsNullOrEmpty(ChangeRow.OldANC) && ChangeRow.OldANC_Q != 0)
                {
                    decimal oldSTK = STKServices.Get(ChangeRow.OldANC, loadedAction.Year, loadedAction.Devision.Factory).STK3;
                    ChangeRow.OldANC_STD = Math.Round(oldSTK * ChangeRow.OldANC_Q, 4, MidpointRounding.AwayFromZero);
                }
                if (!string.IsNullOrEmpty(ChangeRow.NewANC) && ChangeRow.NewANC_Q != 0)
                {
                    decimal newSTK = STKServices.Get(ChangeRow.NewANC, loadedAction.Year, loadedAction.Devision.Factory).STK3;
                    ChangeRow.NewANC_STD = Math.Round(newSTK * ChangeRow.NewANC_Q, 4, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void LoadedAction_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ActionBase.Calculation))
            {
                SumVisibility = (sender as ActionBase).Calculation is CalculationMethod.Mix or CalculationMethod.PNC
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public ActionBase LoadedAction { get => loadedAction; set { loadedAction = value; OnPropertyChange(); } }

        private decimal oldSTKSum;
        private decimal newSTKSum;
        private decimal deltaSum;
        private decimal estymationSum;
        private decimal calculationSum;
        private Visibility sumVisibility = Visibility.Collapsed;

        public Visibility SumVisibility { get => sumVisibility; set { sumVisibility = value; OnPropertyChange(); } }


        public decimal CalculationSum { get => calculationSum; set { calculationSum = value; OnPropertyChange(); } }
        public decimal EstymationSum { get => estymationSum; set { estymationSum = value; OnPropertyChange(); } }
        public decimal DeltaSum { get => deltaSum; set { deltaSum = value; OnPropertyChange(); } }
        public decimal NewSTKSum { get => newSTKSum; set { newSTKSum = value; OnPropertyChange(); } }
        public decimal OldSTKSum { get => oldSTKSum; set { oldSTKSum = value; OnPropertyChange(); } }


        public ICommand PlusButton { get; private set; }
        public ICommand MinusButton { get; private set; }

        public void Plus()
        {
            if (loadedAction.ANCChanges.Count <= 10)
            {
                loadedAction.ANCChanges.Add(new());
            }
        }
        public void Minus()
        {
            if (loadedAction.ANCChanges.Count == 1)
            {
                loadedAction.ANCChanges.Remove(loadedAction.ANCChanges.Last());
                loadedAction.ANCChanges.Add(new());
            }
            else
            {
                loadedAction.ANCChanges.Remove(loadedAction.ANCChanges.Last());
            }

        }
    }
}
