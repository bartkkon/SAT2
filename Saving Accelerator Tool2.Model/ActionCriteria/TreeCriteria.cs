using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.ActionCriteria
{
    public class TreeCriteria
    {
        public decimal Year { get; set; }
        public Factories Factory { get; set; }
        public Devision Devisions { get; set; }
        public Leaders Leader { get; set; }
        public Condition Conditions { get; set; }
    }
}
