using CountersPlus.Counters.Interfaces;

namespace CenterDistanceCounter.Interfaces
{
     public interface ExpandedICounter:IEventHandler
    {
        void ExpandedCounterInit();

        void ExpandedCounterDestroy();
    }
}
