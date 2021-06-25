using System.Windows.Controls;

using Saving_Accelerator_Tools2.ViewModels;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class AdminPage : Page
    {
        public AdminPage(AdminViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
