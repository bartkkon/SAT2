using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.Model.Others
{
    public class Factories
    {
        public Factories()
        {
            Devisions = new HashSet<Devision>();
        }

        [Key]
        public int ID { get; set; }
        public string Plant { get; set; }
        public bool Active { get; set; }

        public ICollection<Devision> Devisions { get; set; }
    }
}
