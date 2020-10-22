using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class Approvals_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        public string Revision { get; set; }
        public int? Month { get; set; }
        public string Devision { get; set; }
        public int Status { get; set; }
    }
}
