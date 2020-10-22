using Saving_Accelerator_Tools2.Models;

namespace Saving_Accelerator_Tools2.Contracts.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
