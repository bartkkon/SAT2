using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class NavigationButtonViewModel : INotifyProperty
    {
        public NavigationButtonViewModel()
        {
            SaveAction = new RelayCommand<object>(Save);
        }

        public ICommand SaveAction
        {
            get; private set;
        }

        private void Save(object obj)
        {
            var Save = new SaveAction2();
            Save.Save();
        }
    }
}
