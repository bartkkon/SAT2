using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Data
{
    public class ConstantVariables
    {
        public int ID { get; set; }
        public Constant Name { get; set; }
        public decimal Year { get; set; }
        public Revisions? Revision { get; set; }
        public Currency Currency { get; set; }
        public Factories Factory { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Value { get; set; }

    }
}
