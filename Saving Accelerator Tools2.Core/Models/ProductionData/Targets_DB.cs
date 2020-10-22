using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class Targets_DB
    {
        [Key]
        public int ID { get; set; }
        public decimal Year { get; set; }
        public string Revision { get; set; }
        public decimal DM { get; set; }
        public decimal PC { get; set; }
        public decimal Electronic { get; set; }
        public decimal Mechanic { get; set; }
        public decimal NVR { get; set; }
    }
}
