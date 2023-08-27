using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Models;

namespace SpecialOffers.Controllers
{
    [Route(("/events"))]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore eventStore;

        public EventFeedController(IEventStore eventStore) => this.eventStore = eventStore;

        [HttpGet("")]
        public async Task<ActionResult<EventFeedEvent[]>> GetEvents([FromQuery] int start, [FromQuery] int end)
        {
            if (start < 0 || end < start)
                return BadRequest();

            return (await this.eventStore.GetEvents(start, end)).ToArray();
        }
    }
}