using Saving_Accelerator_Tools2.Model;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Users;
using System.Collections.Generic;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Users
{
    public interface IUserServices
    {
        public void Add(User newUser);
        public ICollection<User> Get();
        public ICollection<User> Get(bool active);
        public void Set(User changeUser);

        public ICollection<User> MailSubscriptions(Subscription subscription, Factories factory);

    }
}
