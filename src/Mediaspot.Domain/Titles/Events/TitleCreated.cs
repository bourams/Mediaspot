using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Titles.Events;

public sealed record TitleCreated(Guid TitleId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
