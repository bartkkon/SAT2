using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Helpers;
using SavingAcceleratorTools2.ProjectModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class CalculationViewModel : Base
    {
        private readonly ActionBase loadedAction;

        public CalculationViewModel(ActionBase LoadedAction)
        {
            loadedAction = LoadedAction;
            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
            Load(null);
        }

        private void LoadedAction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ActionBase.Calculation))
            {
                Load(sender as ActionBase);
            }
        }

        private bool aNC;
        private bool mix;
        private bool pNC;
        private bool pNCSpecial;

        public bool PNCSpecial
        {
            get => pNCSpecial;
            set
            {
                if (pNCSpecial != value)
                {
                    pNCSpecial = value;
                    if (pNCSpecial && loadedAction.Calculation != CalculationMethod.PNCSpecial)
                    {
                        loadedAction.Calculation = CalculationMethod.PNCSpecial;
                        PNC = false;
                        Mix = false;
                        ANC = false;
                    }
                }
                OnPropertyChange();
            }
        }

        public bool PNC
        {
            get => pNC;
            set
            {
                if (pNC != value)
                {
                    pNC = value;
                    if (pNC && loadedAction.Calculation != CalculationMethod.PNC)
                    {
                        loadedAction.Calculation = CalculationMethod.PNC;
                        PNCSpecial = false;
                        Mix = false;
                        ANC = false;
                    }
                    if(!pNC)
                    {
                        loadedAction.PNCs.Clear();
                    }
                }
                OnPropertyChange();
            }
        }

        public bool Mix
        {
            get => mix;
            set
            {
                if (mix != value)
                {
                    mix = value;
                    if (mix && loadedAction.Calculation != CalculationMethod.Mix)
                    {
                        loadedAction.Calculation = CalculationMethod.Mix;
                        PNCSpecial = false;
                        PNC = false;
                        ANC = false;
                    }
                }
                OnPropertyChange();
            }
        }

        public bool ANC
        {
            get => aNC;
            set
            {
                if (aNC != value)
                {
                    aNC = value;
                    if (aNC && loadedAction.Calculation != CalculationMethod.ANC)
                    {
                        loadedAction.Calculation = CalculationMethod.ANC;
                        PNCSpecial = false;
                        PNC = false;
                        Mix = false;
                    }
                }
                OnPropertyChange();
            }
        }

        private void Load(ActionBase? action)
        {
            ActionBase action2 = action != null ? action : loadedAction;
            switch (action2.Calculation)
            {
                case CalculationMethod.ANC:
                    ANC = true;
                    break;
                case CalculationMethod.PNC:
                    PNC = true;
                    break;
                case CalculationMethod.Mix:
                    Mix = true;
                    break;
                case CalculationMethod.PNCSpecial:
                    PNCSpecial = true;
                    break;
                default:
                    ANC = true;
                    break;
            }
        }
    }
}
