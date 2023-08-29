namespace SpecialOffers.Models
{
    public interface IEventStore
    {
        Task RaiseEvent(string name, object content);
        Task<IEnumerable<EventFeedEvent>> GetEvents(int start, int end);
    }
    public class EventStore : IEventStore
    {
        private static long currentSequenceNumber = 0;
        private static readonly IList<EventFeedEvent> Database = new List<EventFeedEvent>();

        public Task RaiseEvent(string name, object content)
        {
            var seqNumber = Interlocked.Increment(ref currentSequenceNumber);
            Database.Add(new EventFeedEvent(seqNumber, DateTimeOffset.UtcNow, name, content));
            return Task.CompletedTask;
        }

        public Task<IEnumerable<EventFeedEvent>> GetEvents(int start, int end)
        {
            return Task.FromResult<IEnumerable<EventFeedEvent>>(
                Database
                .Where(e => start <= e.SequenceNumber && e.SequenceNumber < end)
                .OrderBy(e => e.SequenceNumber));
        }
    }
}
