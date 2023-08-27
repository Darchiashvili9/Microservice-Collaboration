namespace SpecialOffers.Models
{
    public interface IEventStore
    {
        void RaiseEvent(string name, object content);
        IEnumerable<EventFeedEvent> GetEvents(int start, int end);
    }

    public class EventStore : IEventStore
    {
        private static long currentSequenceNumber = 0;
        private static readonly IList<EventFeedEvent> Database = new List<EventFeedEvent>();

        public void RaiseEvent(string name, object content)
        {
            var seqNumber = Interlocked.Increment(ref currentSequenceNumber);
            Database.Add(new EventFeedEvent(seqNumber, DateTimeOffset.UtcNow, name, content));
        }

        public IEnumerable<EventFeedEvent> GetEvents(int start, int end)
        {
            var ret = Database.Where(e => start <= e.SequenceNumber && e.SequenceNumber < end).OrderBy(e => e.SequenceNumber);
            return ret;
        }
    }

    public class FakeEventStore : IEventStore
    {
        public Task RaiseEvent(string name, object content)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<EventFeedEvent>> GetEvents(int start, int end)
        {
            if (start > 100)
                return Task.FromResult(return Task.FromResult(Enumerable.Range(start, end - start)
                    .Select(i => new EventFeedEvent(i, DateTimeOffset.UtcNow, "some event", new object())));
        }
    }
}
