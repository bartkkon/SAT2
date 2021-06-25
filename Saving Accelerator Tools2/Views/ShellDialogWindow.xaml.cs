using System.Windows.Controls;
using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.IServices.Base.Views;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class ShellDialogWindow : MetroWindow, IShellDialogWindow
    {
        public ShellDialogWindow(ShellDialogViewModel viewModel)
        {
            InitializeComponent();
            viewModel.SetResult = OnSetResult;
            DataContext = viewModel;
        }

        public Frame GetDialogFrame()
            => dialogFrame;

        private void OnSetResult(bool? result)
        {
            DialogResult = result;
            Close();
        }
    }
}
