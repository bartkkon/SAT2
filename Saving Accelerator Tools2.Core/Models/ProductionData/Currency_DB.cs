using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class Currency_DB
    {
        [Key]
        public decimal Year { get; set; }
        [Column(TypeName = "decimal(7,5")]
        public decimal Euro { get; set; }
        [Column(TypeName = "decimal(6,4")]
        public decimal Dolar { get; set; }
        [Column(TypeName = "decimal(6,4")]
        public decimal Sek { get; set; }
        [Column(TypeName = "decimal(6,4")]
        public decimal ECCC { get; set; }
    }
}
