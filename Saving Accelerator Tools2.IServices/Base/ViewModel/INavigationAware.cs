namespace Saving_Accelerator_Tools2.IServices.Base.ViewModel
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom();
    }
}
