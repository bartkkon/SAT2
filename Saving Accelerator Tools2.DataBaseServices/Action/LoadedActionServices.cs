using Saving_Accelerator_Tools2.DataBaseIServices.ActionService;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Model.Action.Sub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.Action
{
    public class LoadedActionServices : ILoadedAction
    {
        private readonly ConnectionContext connection;
        private ActionBase loadedAction;
        private readonly ILoginUserService loginUser;
        private readonly ILeadersServices leadersServices;

        public LoadedActionServices(ConnectionContext connection, ActionBase loadedAction, ILoginUserService loginUser, ILeadersServices leadersServices)
        {
            this.connection = connection;
            this.loadedAction = loadedAction;
            this.loginUser = loginUser;
            this.leadersServices = leadersServices;
            //loaded = new 

        }

        public void New()
        {
            ActionBase NewRecord = new()
            {
                Devision = loginUser.Get().Devision,
                Leader = leadersServices.Get(loginUser.Get().Name, loginUser.Get().Surname),
                Platforms = new List<Platform>(),
            };
            PropertyCopier<ActionBase, ActionBase>.Copy(NewRecord, loadedAction);
        }
    }

    public class PropertyCopier<TParent, TChild> where TParent : class
                                        where TChild : class
    {
        public static void Copy(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType && childProperty.CanWrite)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
    }
}
