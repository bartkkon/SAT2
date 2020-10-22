using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class Currency_DB
    {
        [Key]
        public decimal Year { get; set; }
        public decimal Euro { get; set; }
        public decimal Dolar { get; set; }
        public decimal Sek { get; set; }
        public decimal ECCC { get; set; }
    }
}
