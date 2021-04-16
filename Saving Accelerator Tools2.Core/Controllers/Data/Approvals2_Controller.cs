using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Data
{
    public class Approvals2_Controller
    {
        public static List<Approvals2_DB> Load_Year(decimal Year, string Plant)
        {
            List<Approvals2_DB> Data = new List<Approvals2_DB>();
            var context = new DataBaseConnetionContext();

            Data = context.Approvals2.Where(record => record.Year == Year && record.Plant == Plant)
                .Include(subrecord => subrecord.Devisions).ThenInclude(subrecord => subrecord.Devision)
                .ToList();

            return Data;
        }
    }
}
