using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Users
{
    public class PageVisibility_DB
    {
        [Key]
        public int ID { get; set; }
        public string Tab { get; set; }

        public virtual List<User_Pages_DB> User_Pages { get; set; } = new List<User_Pages_DB>();
    }
}
