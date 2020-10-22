using System.Windows.Controls;

using Saving_Accelerator_Tools2.ViewModels;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class SummaryPage : Page
    {
        public SummaryPage(SummaryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
