using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.ViewModels.Helpers;
using SavingAcceleratorTools2.ProjectModels.Action;

namespace Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram
{
    public class ActionViewModel : Observable
    {
        public ActionViewModel(ActionBase LoadedAction)
        {
            loadedAction = LoadedAction;
            loadedAction.PropertyChanged += LoadedAction_PropertyChanged;
        }

        private void LoadedAction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ActionBase.Calculation))
            {
                CalculationMethodPage = (sender as ActionBase).Calculation switch
                {
                    CalculationMethod.PNC => "Action2/PNCView.xaml",
                    CalculationMethod.Mix => "",
                    CalculationMethod.PNCSpecial => "",
                    CalculationMethod.ANC => string.Empty,
                    _ => string.Empty,
                };
            }
        }

        private readonly ActionBase loadedAction;
        private string calculationMethodPage;

        public string CalculationMethodPage
        {
            get => calculationMethodPage; set
            {
                calculationMethodPage = value;
                OnPropertyChanged(nameof(CalculationMethodPage));
            }
        }


    }
}
