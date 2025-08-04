using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Titles;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests;

public class CreateAssetHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Asset_When_ExternalId_Is_Unique()
    {
        var title = new Title("My Movie", "Desc", new DateOnly(2025, 09, 03), TitleType.Movie);
        var titleId = title.Id;

        var assetRepo = new Mock<IAssetRepository>();
        var titleRepo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();
        titleRepo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(title);
        assetRepo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((Asset?)null);
        assetRepo.Setup(r => r.AddAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new CreateAssetHandler(assetRepo.Object, titleRepo.Object, uow.Object);
        var cmd = new CreateAssetCommand("ext-unique", titleId, "title", "desc", "en");

        var id = await handler.Handle(cmd, CancellationToken.None);

        id.ShouldNotBe(Guid.Empty);
        assetRepo.Verify(r => r.AddAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_ExternalId_Exists()
    {
        var assetRepo = new Mock<IAssetRepository>();
        var titleRepo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();
        assetRepo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Asset("ext-unique", "titleId", new Metadata("t", null, null)));
        var handler = new CreateAssetHandler(assetRepo.Object, titleRepo.Object, uow.Object);
        var cmd = new CreateAssetCommand("ext-unique", new Guid(), "title", "desc", "en");

        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }
}
