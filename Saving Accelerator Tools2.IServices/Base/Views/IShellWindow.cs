using System.Windows.Controls;

namespace Saving_Accelerator_Tools2.IServices.Base.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
