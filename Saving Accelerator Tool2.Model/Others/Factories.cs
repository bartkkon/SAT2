using Saving_Accelerator_Tools2.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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
        public ICollection<Tag> Tags { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<StandardCost> StandardCosts { get; set; }

        public ICollection<ANC> ANCs { get; set; }
        public ICollection<PNC> PNCs { get; set; }
        public ICollection<PNCPlatform> PNCPlatforms { get; set; }
    }
}
