using EventService.Models;

namespace EventService.Serivces
{
    public interface IEventStore
    {
       void GetEvents(long firstSequence, long lastSequence);
       void Raise(object EventRaised);
    }
}
