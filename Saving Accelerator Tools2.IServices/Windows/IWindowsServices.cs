using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.IServices.Windows
{
    public interface IWindowsServices
    {
        public void MonthlyQuantityWindow(decimal Year,
            Months Month,
            Factories Factory,
            IANCsServices ANCsServices,
            IPNCsServices PNCsServices,
            IPNCPlatformServices pNCPlatformServices);

        public void RevisionQuantityWindow(decimal Year,
            Revisions Revision,
            Factories Factory,
            IANCsServices ANCsServices,
            IPNCsServices PNCsServices,
            IPNCPlatformServices pNCPlatformServices,
            IMessageBoxService messageBoxSerices);
    }
}
