using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class ANCChange_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string OldANC { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal OldANCQuantity { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal OldSTK { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string NewANC { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal NewANCQuantity { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal NewSTK { get; set; }

        [Column(TypeName = "decimal(8,4)")]
        public decimal Delta { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal UserEstymacja { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Percent { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Estymacja { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string NextANC1 { get; set; }
        [Column(TypeName = "varchar(9)")]
        public string NextANC2 { get; set; }

        public virtual List<Action_ANCChage_InterTable> Action_ANCChange { get; set; } = new List<Action_ANCChage_InterTable>();
    }
}
