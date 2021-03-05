using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable
{
    public class PNCSPecial_PNC_ANC_InterTable
    {
        public int PNC_ID { get; set; }
        public PNCSpecial_PNC_DB PNCChange { get; set; }

        public int ANC_ID { get; set; }
        public PNCSpecial_ANC_DB ANCChange { get; set; }
    }
}
