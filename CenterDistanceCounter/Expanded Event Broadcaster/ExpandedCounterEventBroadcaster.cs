using CenterDistanceCounter.Interfaces;

namespace CenterDistanceCounter.Expanded_Event_Broadcaster
{
    class ExpandedCounterEventBroadcaster:ExpandedEventBroadcaster<ExpandedICounter>
    {
        public override void Initialize()
        {
            foreach (ExpandedICounter counter in EventHandlers)
            {
                counter.ExpandedCounterInit();
            }
        }

        public override void Dispose()
        {
            foreach (ExpandedICounter counter in EventHandlers)
            {
                counter.ExpandedCounterDestroy();
            }
        }
    }
}
