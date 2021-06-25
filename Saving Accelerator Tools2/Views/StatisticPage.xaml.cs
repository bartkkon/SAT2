using System.Windows.Controls;

using Saving_Accelerator_Tools2.ViewModels;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class StatisticPage : Page
    {
        public StatisticPage(StatisticViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
