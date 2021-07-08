using Saving_Accelerator_Tools2.IServices.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saving_Accelerator_Tools2.MessageBoxes
{
    public class MessageBoxServices : IMessageBoxService
    {
        public MessageBoxResult Ask(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
        }

        public void Error(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }

        public void ShowConfirmation(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
    }
}
