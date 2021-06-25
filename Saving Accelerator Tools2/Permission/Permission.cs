using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Permission
{
    public class Permission
    {
        private static Permission_DB _instance;
        private static readonly object syncRoot = new object();

        public static Permission_DB Check
        {
            get
            {
                if(_instance == null)
                {
                    lock (syncRoot)
                    {
                        if(_instance == null)
                        {
                            _instance = new Permission_DB();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
