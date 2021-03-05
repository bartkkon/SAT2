using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class PNCSpecial_PNC_DB
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "char(9)")]
        public string PNC { get; set; }
        [Column(TypeName = "decimal(7,4)")]
        public decimal ECCC { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Old_STK { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal New_STK { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Delta { get; set; }

        public virtual List<Action_PNCSpecial_InterTable> Action_PNCSpecial { get; set; } = new List<Action_PNCSpecial_InterTable>();
        public virtual List<PNCSPecial_PNC_ANC_InterTable> PNC_ANC_Special { get; set; } = new List<PNCSPecial_PNC_ANC_InterTable>();
    }
}
