﻿namespace SpecialOffers.Models
{
    public record EventFeedEvent(
                                long SequenceNumber,
                                DateTimeOffset OccuredAt,
                                string Name,
                                object Content);
}
