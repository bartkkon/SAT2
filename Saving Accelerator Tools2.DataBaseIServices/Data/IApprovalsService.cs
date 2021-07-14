using Saving_Accelerator_Tools2.Model.Approvals;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Data
{
    public interface IApprovalsService
    {
        public void Set(Approval NewRecord);
        public void Update(Approval updateRecord);
        public ICollection<Approval> Get(Approval_SearchCriteria criteria);
        public Approval GetOpen(Factories factories);
    }
}
