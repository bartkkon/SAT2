using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users.InterTable
{
    public class User_Pages_DB
    {
        public int UserID { get; set; }
        public Users_DB User { get; set; }

        public int PageID { get; set; }
        public PageVisibility_DB Page { get; set; }
    }
}
