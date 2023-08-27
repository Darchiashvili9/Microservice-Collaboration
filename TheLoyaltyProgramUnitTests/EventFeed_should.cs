using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using SpecialOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheLoyaltyProgramUnitTests
{
    public class EventFeed_should : IDisposable
    {
        private readonly HttpClient sut;
        private readonly WebApplicationFactory<LoyalityProgram.Controllers.UsersController> host;

        public EventFeed_should()
        {
            this.host = new WebApplicationFactory<LoyalityProgram.Controllers.UsersController>();
            this.sut = host.CreateClient();
        }

        [Fact]
        public async Task return_events_when_from_event_store()
        {
            var actual = await this.sut.GetAsync("/events?start=0&end=100");

            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
            var eventFeedEvents =
              await JsonSerializer.DeserializeAsync<IEnumerable<EventFeedEvent>>(await actual.Content.ReadAsStreamAsync())
              ?? Enumerable.Empty<EventFeedEvent>();
            Assert.Equal(100, eventFeedEvents.Count());
        }

        [Fact]
        public async Task return_empty_response_when_there_are_no_more_events()
        {
            var actual = await this.sut.GetAsync("/events?start=200&end=300");

            var eventFeedEvents = await JsonSerializer.DeserializeAsync<IEnumerable<EventFeedEvent>>(await actual.Content.ReadAsStreamAsync());
            Assert.Empty(eventFeedEvents);
        }

        public void Dispose()
        {
            this.sut?.Dispose();
            this.host?.Dispose();
        }
    }

    public class FakeEventStore : IEventStore
    {
        public Task RaiseEvent(string name, object content) =>
          throw new NotImplementedException();

        public Task<IEnumerable<EventFeedEvent>> GetEvents(int start, int end)
        {
            if (start > 100)
                return Task.FromResult(Enumerable.Empty<EventFeedEvent>());

            return Task.FromResult(Enumerable
              .Range(start, end - start)
              .Select(i => new EventFeedEvent(i, DateTimeOffset.UtcNow, "some event", new object())));
        }
    }
}