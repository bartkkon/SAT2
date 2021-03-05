using Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class PNCSpecial_ANC_DB
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "char(9)")]
        public string Old_ANC { get; set; }
        [Column(TypeName = "decimal(7,4)")]
        public decimal Old_Q { get; set; }
        [Column(TypeName = "char(9)")]
        public string New_ANC { get; set; }
        [Column(TypeName = "decimal(7,4)")]
        public decimal New_Q { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Old_STK { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal New_STK { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Delta { get; set; }

        public virtual List<PNCSPecial_PNC_ANC_InterTable> PNC_ANC_Special { get; set; } = new List<PNCSPecial_PNC_ANC_InterTable>();
    }
}
