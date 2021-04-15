using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.ProductionData
{
    public class Approvals2_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string Revision { get; set; }
        public int? Month { get; set; }
        public string Plant { get; set; }

        public virtual List<Approvals_IT> Devisions { get; set; } = new List<Approvals_IT>();
    }

    public class Approvals_IT
    { 
        public int Approvals_ID { get; set; }
        public Approvals2_DB Approvals { get; set; }

        public int Devision_ID { get; set; }
        public Approvals_Devision_DB Devision { get; set; }
    }


    public class Approvals_Devision_DB
    {
        [Key]
        public int ID { get; set; }

        public string Devision { get; set; }
        public bool Status { get; set; }
        public string By { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Approvals_IT> Devisions { get; set; } = new List<Approvals_IT>();
    }

}
