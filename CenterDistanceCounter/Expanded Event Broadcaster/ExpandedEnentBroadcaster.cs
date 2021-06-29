using System;
using System.Collections.Generic;
using CountersPlus.Counters.Interfaces;
using Zenject;

namespace CenterDistanceCounter.Expanded_Event_Broadcaster
{
    internal abstract class ExpandedEventBroadcaster<T> : IInitializable, IDisposable where T : IEventHandler
    {
        [Inject] protected List<T> EventHandlers = new List<T>();

        public abstract void Initialize();

        public abstract void Dispose();
    }
}
