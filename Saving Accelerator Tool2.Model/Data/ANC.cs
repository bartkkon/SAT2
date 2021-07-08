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
    public class ANC
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar(9)")]
        public string Item { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        public Revisions Revision { get; set; }

        public Months Month { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        public Factories Factory { get; set; }
    }
}
