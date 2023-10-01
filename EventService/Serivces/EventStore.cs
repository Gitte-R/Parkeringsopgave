using EventService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventService.Serivces
{
    public class EventStore : IEventStore
    {
        private static readonly Dictionary<int, Object> EventDatabase = new Dictionary<int, Object>();

        public void Raise(Object EventRaised)
        {
            var SeqNumber = 1;

            if (EventDatabase.Count() > 0)
            {
                SeqNumber = EventDatabase.Last().Key + 1;
            }
            
            EventDatabase.Add(SeqNumber, EventRaised);
            
        }

        public void GetEvents(long firstSequence, long lastSequence)
        {
            EventDatabase.Where(e => e.Key >= firstSequence && e.Key <= lastSequence);

        }


    }
}
