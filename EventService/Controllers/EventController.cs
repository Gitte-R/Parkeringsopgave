using EventService.Models;
using EventService.Serivces;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventStore _eventStore;
        public EventController(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }


        [HttpGet("")]
        public void Get([FromQuery] long start, [FromQuery] long end = long.MaxValue)
        {
            _eventStore.GetEvents(start, end);
        }

    }
}