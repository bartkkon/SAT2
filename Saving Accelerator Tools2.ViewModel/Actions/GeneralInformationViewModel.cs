using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.ViewModel.Actions
{
    public class GeneralInformationViewModel : Base
    {
        private ActionBase loadedAction;

        public GeneralInformationViewModel(ActionBase LoadedAction)
        {
            loadedAction = LoadedAction;
        }

        public ActionBase LoadedAction
        {
            get => loadedAction;
            set { loadedAction = value; OnPropertyChange(); }
        }
    }
}
