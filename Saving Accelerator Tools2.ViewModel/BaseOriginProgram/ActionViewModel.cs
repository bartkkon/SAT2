using Saving_Accelerator_Tools2.ViewModels.Helpers;

namespace Saving_Accelerator_Tools2.ViewModels.BaseOriginProgram
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
