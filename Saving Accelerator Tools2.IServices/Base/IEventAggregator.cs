using Prism.Events;

namespace Saving_Accelerator_Tools2.IServices.Base
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}
