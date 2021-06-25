using System;
using System.Windows.Input;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using Saving_Accelerator_Tools2.ViewModels.Helpers;

namespace Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram
{
    public class ShellDialogViewModel : Observable
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(OnClose));

        public Action<bool?> SetResult { get; set; }

        public ShellDialogViewModel()
        {
        }

        private void OnClose()
        {
            bool result = true;
            SetResult(result);
        }
    }
}
