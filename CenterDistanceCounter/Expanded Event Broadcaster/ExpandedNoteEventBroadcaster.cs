using CenterDistanceCounter.Interfaces;
using Zenject;

namespace CenterDistanceCounter.Expanded_Event_Broadcaster
{
        /// <summary>
        /// A <see cref="EventBroadcaster{T}"/> that broadcasts events relating to note cutting and missing.
        /// </summary>
        internal class ExpandedNoteEventBroadcaster : ExpandedEventBroadcaster<ExpandedINoteEventHandler>
        {
            [Inject] private BeatmapObjectManager beatmapObjectManager;

            public override void Initialize()
            {
                beatmapObjectManager.noteWasCutEvent += ExpandedNoteWasCutEvent;
                beatmapObjectManager.noteWasMissedEvent += ExpandedNoteWasMissedEvent;
            }

            private void ExpandedNoteWasCutEvent(NoteController data, in NoteCutInfo noteCutInfo)
            {
                foreach (ExpandedINoteEventHandler noteEventHandler in EventHandlers)
                {
                    noteEventHandler?.ExpandedOnNoteCut(data, noteCutInfo);
                }
            }

            private void ExpandedNoteWasMissedEvent(NoteController data)
            {
                foreach (ExpandedINoteEventHandler noteEventHandler in EventHandlers)
                {
                    noteEventHandler?.ExpandedOnNoteMiss(data.noteData);
                }
            }

            public override void Dispose()
            {
                beatmapObjectManager.noteWasCutEvent -= ExpandedNoteWasCutEvent;
                beatmapObjectManager.noteWasMissedEvent -= ExpandedNoteWasMissedEvent;
            }
        }
}
