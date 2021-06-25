using SavingAcceleratorTools2.ProjectModels;

namespace Saving_Accelerator_Tools2.IServices.Base
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
