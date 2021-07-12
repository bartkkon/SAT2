using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.IServices.Windows;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Windows;
using Saving_Accelerator_Tools2.Windows.Views;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Services
{
    public class ExtraWindowsServices : IWindowsServices
    {
        public void MonthlyQuantityWindow(decimal Year, Months Month, Factories Factory, IANCsServices ANCsServices, IPNCsServices PNCsServices, IPNCPlatformServices pNCPlatformServices)
        {
            var QuantityWindow = new MonthlyQuantityWindow();
            QuantityWindow.DataContext = new MonthlyQuantityViewModel(Year, Month, Factory, ANCsServices, PNCsServices, pNCPlatformServices);
            QuantityWindow.Show();
        }

        public void RevisionQuantityWindow(decimal Year, Revisions Revision, Factories Factory, IANCsServices ANCsServices, IPNCsServices PNCsServices, IPNCPlatformServices pNCPlatformServices, IMessageBoxService messageBoxSerices)
        {
            var QuantityWindow = new RevisionQuantityWindow();
            QuantityWindow.DataContext = new RevisionQuantityViewModel(Year, Revision, Factory, ANCsServices, PNCsServices, pNCPlatformServices, messageBoxSerices);
            QuantityWindow.Show();
        }
    }
}
