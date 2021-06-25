using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Saving_Accelerator_Tools2.Contracts.Services
{
    public class INotifyProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RisePropoertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
