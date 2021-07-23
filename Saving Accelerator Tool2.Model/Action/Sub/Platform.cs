using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Action.Sub
{
    public class Platform
    {
        public int ID { get; set; }
        public Structure Structure { get; set; }
        public Installation Installation { get; set; }

    }
}
