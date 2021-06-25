using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class Calculation_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "char(9)")]
        public string Item { get; set; }
        [Column(TypeName = "char(3)")]
        public string Revision { get; set; }
        public int Month { get; set; }
        public bool CarryOver { get; set; }

        [Column (TypeName = "decimal(16,4)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal Savings { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal ECCC { get; set; }

        public virtual List<Action_Results_InterTable> Action_Results { get; set; } = new List<Action_Results_InterTable>();
    }
}
