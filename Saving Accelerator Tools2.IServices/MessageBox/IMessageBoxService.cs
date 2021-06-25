using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.IServices.MessageBox
{
    public interface IMessageBoxService
    {
        public void ShowConfirmation(string title, string message);
    }
}
