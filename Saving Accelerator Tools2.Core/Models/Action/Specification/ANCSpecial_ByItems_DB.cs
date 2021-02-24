using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class ANCSpecial_ByItems_DB
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "char(9)")]
        public string Item { get; set; }
        public bool Plus { get; set; }
        public bool Minus { get; set; }

        public virtual List<Action_ANCChange_Items_InterTable> Action_ANCChange_Items { get; set; } = new List<Action_ANCChange_Items_InterTable>();
    }
}
