
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Models.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.LoadAction
{
    public class LoadedAction : Action_DB
    {
        private static Action_DB _instance;
        private static readonly object syncRoot = new object();
        //public static event PropertyChangedEventHandler PropertyChanged;

        protected LoadedAction() { }

        public static Action_DB Current {
            get {
                if (_instance == null) {
                    lock (syncRoot) {
                        if (_instance == null) {
                            _instance = new LoadedAction();
                            Action_Controller.Load(1);
                        }
                    }
                }
                return _instance;
            }
        }

        public static bool New {
            get {
                lock (syncRoot) {
                    _instance = new LoadedAction();
                }
                return true;
            }
        }
        public static void Load(int ActionID)
        {
            Action_Controller.Load(ActionID);
            //OnPropoertyChange("Name");
        }

        //protected static void OnPropoertyChange(string Name)
        //{
        //    PropertyChanged?.Invoke(_instance, new PropertyChangedEventArgs(Name));
        //}

        //{Binding Source={x:Static LoadedAction.Current}, Path=Name, Mode=TwoWay}
    }
}
