using System;

using Saving_Accelerator_Tools2.Helpers;

namespace Saving_Accelerator_Tools2.ViewModels
{
    public class ActionViewModel : Observable
    {
        public ActionViewModel()
        {

        }
        private string _Test = "TEST";
        public string TEST { get { return _Test; } set { _Test = value; } }
    }
}
