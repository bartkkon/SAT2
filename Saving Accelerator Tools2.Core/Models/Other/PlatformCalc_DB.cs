using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other
{
    public class PlatformCalc_DB
    {
        [Key]
        public int ID { get; set; }
        public string Platform { get; set; }
        public string Installation { get; set; }
        public bool Active { get; set; }

        public virtual List<Action_ANCChangePlatform_InterTable> Action_ANCChange_Platform { get; set; } = new List<Action_ANCChangePlatform_InterTable>();

        /* 1    All
         * 2    D45
         * 3    DMD
         * 4    DMD FS
         * 5    DMD FI
         * 6    DMD BI
         * 7    DMD FSBU
         * 8    D45 FS
         * 9    D45 FI
         * 10   D45 BI
         * 11   D45 FSBU
         */
    }
}
