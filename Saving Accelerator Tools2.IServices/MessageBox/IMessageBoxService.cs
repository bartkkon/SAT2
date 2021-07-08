using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saving_Accelerator_Tools2.IServices.MessageBox
{
    public interface IMessageBoxService
    {
        public void ShowConfirmation(string title, string message);

        public MessageBoxResult Ask(string title, string message);

        public void Error(string title, string message);
    }
}
