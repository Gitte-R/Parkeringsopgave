using Microsoft.AspNetCore.Mvc;
using ParkingService.Events;
using System.Linq;

namespace ParkingService.Controllers
{
    [Route("/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventStore eventStore;
        public EventController(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }


        [HttpGet("test")]
        public ActionResult<List<EventFeedEvent>> GetEvents([FromQuery] int start, [FromQuery] int end)
        {
            if (start < 0 || end < start)
            {
                return BadRequest();
            }
            return eventStore.GetEvents(start, end).ToList();
        }
    }
}
