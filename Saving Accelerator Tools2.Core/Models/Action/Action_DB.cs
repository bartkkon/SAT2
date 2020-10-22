
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action
{
    public class Action_DB
    {

        [Key]
        public int ID { get; set; }
        public int ActionID { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName ="varchar(1000)")]
        public string Description { get; set; }
        [Column(TypeName ="decimal(4,0)")]
        public decimal StartYear { get; set; }
        public int Month { get; set; }

        public virtual List<Action_Devision_InterTable> Action_Devision { get; set; } = new List<Action_Devision_InterTable>();
        public virtual List<Action_Plant_InterTable> Action_Plant { get; set; } = new List<Action_Plant_InterTable>();

    }
}
