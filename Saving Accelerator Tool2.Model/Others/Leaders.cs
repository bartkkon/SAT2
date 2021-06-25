using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Others
{
    public class Leaders
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public bool Active { get; set; }

        public int DevisionID { get; set; }
        public Devision Devision { get; set; }
    }

    public class LeadersSearchCriteria
    {
        public int? DevisionID { get; set; }
        public int? FactoryID { get; set; }
        public bool? Active { get; set; }
    }
}
