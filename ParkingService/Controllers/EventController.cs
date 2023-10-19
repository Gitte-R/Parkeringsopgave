using Microsoft.AspNetCore.Mvc;
using ParkingService.Events;

namespace ParkingService.Controllers
{

    public class EventController : Controller
    {
        private readonly IEventStore eventStore;
        public EventController(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }


        [HttpGet("/events")]
        public ActionResult<EventFeedEvent[]> GetEvents([FromQuery] int start, [FromQuery] int end)
        {
            if (start < 0 || end < start)
            {
                return BadRequest();
            }
            return eventStore.GetEvents(start, end).ToArray();
        }
    }
}
