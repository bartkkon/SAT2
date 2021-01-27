using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Contracts.Services
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}
