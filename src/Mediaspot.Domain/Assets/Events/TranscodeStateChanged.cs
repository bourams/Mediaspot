using Mediaspot.Domain.Common;
using Mediaspot.Domain.Transcoding;

namespace Mediaspot.Domain.Assets.Events;

public record TranscodeStateChanged(Guid transcodeId, TranscodeStatus status) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}

