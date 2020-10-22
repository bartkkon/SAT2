using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.User
{
    public class User : Users_DB
    {
        private static Users_DB _instance;
        private static readonly object syncRoot = new object();

        protected User()
        {

        }

        public static Users_DB Logged
        {
            get
            {
                if(_instance == null)
                {
                    lock (syncRoot) {
                        if (_instance == null)
                        {
                            _instance = new User();
                            _instance = UserController.LoadUser();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
