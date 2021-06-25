using System.Windows.Controls;

using Saving_Accelerator_Tools2.ViewModels;
using Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class Summary_DetailPage : Page
    {
        public Summary_DetailPage(Summary_DetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
