using Mediaspot.Application.Common;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.Transcode;

public sealed class TranscodeRequestHandler(IAssetRepository repo, IUnitOfWork uow)
    : IRequestHandler<TranscodeRequestCommand>
{
    public async Task Handle(TranscodeRequestCommand request, CancellationToken cancellationToken)
    {
        var asset = await repo.GetAsync(request.AssetId, cancellationToken) ?? throw new KeyNotFoundException("Asset not found");

        asset.Transcode(request.MediaFileId, request.Preset);
    }
}
