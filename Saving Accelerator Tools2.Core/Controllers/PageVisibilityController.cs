using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers
{
    public class PageVisibilityController
    {
        public static ICollection<PageVisibility_DB> LoadPages()
        {
            var context = new DataBaseConnetionContext();
            return context.Page.ToList();
        }
    }
}
