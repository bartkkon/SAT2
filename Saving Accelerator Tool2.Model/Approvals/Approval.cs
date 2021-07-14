using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Approvals
{
    public class Approval
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }
        public string ApproveBy { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateDate { get; set; }
        public Revisions Revision { get; set; }
        public Months? Month { get; set; }
        public decimal Year { get; set; }

        public ICollection<TeamApp> Teams { get; set; }

    }

    public class Approval_SearchCriteria
    {
        public Revisions? Revision { get; set; }
        public Months? Month { get; set; }
        public Status? Status { get; set; }
        public decimal? Year { get; set; }
        public int? Factory { get; set; }
    }
}
