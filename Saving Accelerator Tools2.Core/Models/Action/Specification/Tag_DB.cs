using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action.Specification
{
    public class Tag_DB
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        [Column(TypeName ="decimal(4,0)")]
        public decimal Start { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Finish { get; set; }

    }
}
