using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed class CreateAssetHandler(IAssetRepository assetRepo, ITitleRepository titleRepo, IUnitOfWork uow)
    : IRequestHandler<CreateAssetCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken ct)
    {
        // Enforce uniqueness of ExternalId
        var existing = await assetRepo.GetByExternalIdAsync(request.ExternalId, ct);
        if (existing is not null)
            throw new InvalidOperationException($"Asset with ExternalId '{request.ExternalId}' already exists.");

        var title = await titleRepo.GetAsync(request.TitleId, ct);
        if (title is null)
            throw new KeyNotFoundException($"Title {request.TitleId} not found.");

        var asset = new Asset(request.ExternalId, request.Title, new Metadata(request.Title, request.Description, request.Language));
        await assetRepo.AddAsync(asset, ct);
        await uow.SaveChangesAsync(ct);
        return asset.Id;
    }
}
