using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Saving_Accelerator_Tools2.DataBaseServices.Users
{
    public class UserServices : IUserServices
    {
        private readonly ConnectionContext connection;

        public UserServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Add(User newUser)
        {
            connection.Add(newUser);
            connection.SaveChanges();
        }

        public ICollection<User> Get()
        {
            return connection.Users.ToList();
        }

        public ICollection<User> Get(bool active)
        {
            return connection.Users.Where(a => a.Active == active).ToList();
        }

        public ICollection<User> MailSubscriptions(Subscription subscription, Factories factory)
        {
            var users = connection.Users.Where(f => f.Devision.Factory == factory).ToList();
            users = users.Where(m => m.MailSubscription.Contains(subscription)).ToList();
            return users;
        }

        public void Set(User changeUser)
        {
            connection.Update(changeUser);
            connection.SaveChanges();
        }
    }
}
