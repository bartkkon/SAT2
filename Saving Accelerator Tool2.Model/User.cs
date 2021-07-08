using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Users;
using System.Collections.Generic;

namespace Saving_Accelerator_Tools2.Model
{
    public class User
    {
        public int ID { get; set; }
        public string BIZLogin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public string Mail { get; set; }
        public AccountTypes Type { get; set; }
        public ICollection<Subscription> MailSubscription { get; set; } = new List<Subscription>();
        public bool Active { get; set; }

        public int DevisionID { get; set; }
        public Devision Devision { get; set; }


    }
}
