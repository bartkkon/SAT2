using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Data
{
    public class StandardCost
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar(9)")]
        public string Item { get; set; }
        public string Description { get; set; }
        public string IDCO { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public decimal STK3 { get; set; }


        public Factories Factory { get; set; }


    }
}
