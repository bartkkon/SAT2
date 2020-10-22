using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class STK_DB
    {
        [Key]
        public int ID { get; set; }

        public string ANC { get; set; }
        public string Description { get; set; }
        public string IDCO { get; set; }
        public decimal Year { get; set; }
        public DateTime Date { get; set; }
        public decimal STD { get; set; }

    }
}
