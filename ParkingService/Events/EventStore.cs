namespace ParkingService.Events
{

    public class EventStore : IEventStore
    {
        private static long currentSequenceNumber = 0;
        private static readonly IList<EventFeedEvent> DatabaseOfEvents = new List<EventFeedEvent>();

        public void RaiseEvent(string name, object content)
        {
            var seqNumber = Interlocked.Increment(ref currentSequenceNumber);
            DatabaseOfEvents.Add(new EventFeedEvent(seqNumber, DateTimeOffset.UtcNow, name, content));
        }

        public IEnumerable<EventFeedEvent> GetEvents(int start, int end)
          => DatabaseOfEvents
            .Where(e => start <= e.sequenceNumber && e.sequenceNumber < end)
            .OrderBy(e => e.sequenceNumber);
    }

    public record EventFeedEvent(long sequenceNumber, DateTimeOffset occuredAt, string name, object content);
}