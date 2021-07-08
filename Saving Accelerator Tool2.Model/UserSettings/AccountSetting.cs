using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.UserSettings
{
    public class AccountSetting
    {
        public int ID { get; set; }
        public AccountTypes Type { get; set; }


        public bool Viewer { get; set; }
        public bool Designer { get; set; }
        public bool TeamApprovals { get; set; }
        public bool DepartmetApprovals { get; set; }
        public bool Editable { get; set; }
    }
}
