using CountersPlus.Counters.Interfaces;

namespace CenterDistanceCounter.Interfaces
{
    public interface ExpandedINoteEventHandler:IEventHandler
    {
            void ExpandedOnNoteCut(NoteController data, NoteCutInfo info);
            void ExpandedOnNoteMiss(NoteData data);
    }
}
