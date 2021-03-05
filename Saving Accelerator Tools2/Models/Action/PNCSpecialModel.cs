using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class PNCSpecialModel
    {
        public int ID { get; set; }
        public string PNC { get; set; }
        public decimal ECCC { get; set; }
        public decimal Old_STK { get; set; }
        public decimal New_STK { get; set; }
        public decimal Delta { get; set; }
        public List<PNCSPecial_ANCChangeModel> ANCChange { get; set; } = new List<PNCSPecial_ANCChangeModel>();

    }

    public class PNCSPecial_ANCChangeModel
    {
        public int ID { get; set; }
        public string Old_ANC { get; set; }
        public decimal Old_Q { get; set; }
        public string New_ANC { get; set; }
        public decimal New_Q { get; set; }
        public decimal Old_STK { get; set; }
        public decimal New_STK { get; set; }
        public decimal Delta { get; set; }
    }
}
