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
    public class PNCPlatform
    {
        public int ID { get; set; }
        [Column(TypeName = "decimal(4,0")]
        public decimal Year { get; set; }
        public Revisions Revision { get; set; }
        public Months Month { get; set; }
        public Structure Structure { get; set; }
        public Installation Installation { get; set; }
        [Column(TypeName ="decimal(16,0")]
        public decimal Quantity { get; set; }

        public Factories Factory { get; set; }
    }
}
