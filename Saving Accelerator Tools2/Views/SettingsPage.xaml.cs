using System.Windows.Controls;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
