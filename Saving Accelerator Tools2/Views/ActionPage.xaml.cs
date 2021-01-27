using System.Windows.Controls;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.ViewModels;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class ActionPage : Page
    {
        public ActionPage(ActionViewModel viewModel)
        {
            InitializeComponent();
            //var Action = new ActionModel();
            DataContext = viewModel;
        }
    }
}
