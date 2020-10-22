using System.Windows.Controls;

namespace Saving_Accelerator_Tools2.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
