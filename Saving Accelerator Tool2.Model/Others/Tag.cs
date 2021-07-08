using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Others
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal From { get; set; }
        public decimal Until { get; set; }
        public bool Active { get; set; }

        public int FactoryID { get; set; }
        public Factories Facotry { get; set; }
    }

    public class TagSearchCriteria
    {
        public decimal? From { get; set; }
        public decimal? Until { get; set; }
        public bool? Active { get; set; }
        public int? FactoryID { get; set; }
    }
}
