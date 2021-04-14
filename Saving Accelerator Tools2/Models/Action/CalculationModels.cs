using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class CalculationModels
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public string Revision { get; set; }
        public bool CarryOver { get; set; }
        public int Month { get; set; }
        public decimal Quantity { get; set; }
        public decimal Savings { get; set; }
        public decimal ECCC { get; set; }
        public bool Update { get; set; }
        public bool ToRemove { get; set; }
    }
}
