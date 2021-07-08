using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Saving_Accelerator_Tools2.Model.Others
{
    public class Devision
    { 
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string DisplayName => $"{Name} {Factory.Plant}";
        public bool Active { get; set; }

        public int FactoryID { get; set; }
        public Factories Factory { get; set; }
        public ICollection<Leaders> Leaders { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
