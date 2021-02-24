using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class CalcByPNC
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName ="char(9)")]
        public string PNC { get; set; }


        public virtual List<Action_PNC_InterTable> Action_PNC { get; set; } = new List<Action_PNC_InterTable>();
    }
}
