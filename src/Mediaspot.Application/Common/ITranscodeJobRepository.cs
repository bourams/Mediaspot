using Mediaspot.Domain.Transcoding;

namespace Mediaspot.Application.Common;

public interface ITranscodeJobRepository
{
    Task AddAsync(TranscodeJob job, CancellationToken ct);
    Task GetAsync(Guid id);
    Task<bool> HasActiveJobsAsync(Guid assetId, CancellationToken ct);
}
