using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tool2.StandardCost.Models
{
    public class StandardCost_DB
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "char(9)")]
        public string ANC { get; set; }
        public string Description { get; set; }
        public string IDCO { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal STD { get; set; }
    }
}
