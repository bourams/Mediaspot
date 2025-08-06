using Mediaspot.Domain.Transcoding;

namespace Mediaspot.Application.Common;

public interface IMessageQueue
{
    Task EnqueueAsync(TranscodeJob job, CancellationToken ct);
    Task<TranscodeJob> DequeueAsync(CancellationToken ct);
    Task<bool> Watch(CancellationToken ct);
}
