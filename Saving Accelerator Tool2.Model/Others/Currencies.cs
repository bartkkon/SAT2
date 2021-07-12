using SavingAcceleratorTools2.ProjectModels.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Others
{
    public class Currencies
    {
        public int ID { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal Year { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Value { get; set; }
        public string Symbol { get; set; }
    }
}
