using Microsoft.Win32;

namespace Saving_Accelerator_Tools2.IServices.MessageBox
{
    public interface IFileDialogService
    {
        OpenFileDialog OpenFile();
        OpenFileDialog OpenFile(string filter);
        OpenFileDialog OpenFile(string filter, string initialDirection);
    }
}
