using Microsoft.Win32;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.MessageBoxes
{
    public class FileDialogService : IFileDialogService
    {
        public OpenFileDialog OpenFile()
        {
            OpenFileDialog openFileDialog = new();
            return openFileDialog.ShowDialog() == true ? openFileDialog : null;
        }
        public OpenFileDialog OpenFile(string filter)
        {
            OpenFileDialog openFileDialog = new() { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog : null;
        }

        public OpenFileDialog OpenFile(string filter, string initialDirection)
        {
            OpenFileDialog openFileDialog = new() { Filter = filter, InitialDirectory = initialDirection };
            return openFileDialog.ShowDialog() == true ? openFileDialog : null;
        }
    }
}
