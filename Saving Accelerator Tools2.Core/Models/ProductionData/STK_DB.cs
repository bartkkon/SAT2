using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class STK_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string ANC { get; set; }
        public string Description { get; set; }
        public string IDCO { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(12,4)")]
        public decimal STD { get; set; }

    }
}
