using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets.Events;
using MediatR;

namespace Mediaspot.Application.Events;

public sealed class TranscodeStatusChangedHandler(ITranscodeJobRepository repo, IUnitOfWork uow)
    : INotificationHandler<TranscodeStateChanged>
{
    public async Task Handle(TranscodeStateChanged @event, CancellationToken ct)
    {
        var transcode = repo.GetAsync(@event.transcodeId) ?? throw new KeyNotFoundException($"Transcode not found with id {@event.transcodeId}");
        transcode.Status = @event.status;

        await uow.SaveChangesAsync(ct);
    }
}
