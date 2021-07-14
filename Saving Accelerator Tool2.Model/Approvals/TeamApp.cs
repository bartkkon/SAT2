using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Approvals
{
    public class TeamApp
    {
        public int ID { get; set; }
        public Devision Devision { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public string ChangeBy { get; set; }
        public DateTime Date { get; set; }

        public Approval Approval { get; set; }
    }
}
