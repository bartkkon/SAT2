using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other.Data
{
    public class PNCTotality_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string Revision { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string Structure { get; set; }
        [Column(TypeName = "varchar(4)")]
        public string Instalation { get; set; }
        public int Month { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Quantity { get; set; }

    }
}
