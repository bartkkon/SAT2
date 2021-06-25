using System.Windows.Controls;
using MahApps.Metro.Controls;
using Saving_Accelerator_Tools2.IServices.Base.Views;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
