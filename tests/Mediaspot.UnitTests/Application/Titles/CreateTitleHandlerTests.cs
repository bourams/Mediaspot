using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Domain.Titles;
using Moq;

namespace Mediaspot.UnitTests.Application.Titles;

public sealed class CreateTitleHandlerTests
{
    private readonly Mock<ITitleRepository> _titleRepo;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly CreateTitleHandler _sut;

    public CreateTitleHandlerTests()
    {
        _titleRepo = new Mock<ITitleRepository>();
        _uow = new Mock<IUnitOfWork>();
        _sut = new CreateTitleHandler(_titleRepo.Object, _uow.Object);

    }
    //Todo: Helper, bogus

    [Fact]
    public async Task Handle_Should_Call_Persistance_Methods()
    {
        // Arrange
        var titleName = "Unique Title";
        var description = "Some description";
        var releaseDate = new DateOnly(2023, 1, 1);
        var type = TitleType.Movie;

        var cmd = new CreateTitleCommand(titleName, description, releaseDate, type);

        _titleRepo.Setup(r => r.ExistsByNameAsync(titleName, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(false);

        _titleRepo.Setup(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var resultId = await _sut.Handle(cmd, CancellationToken.None);

        //Assert
        _titleRepo.Verify(r => r.ExistsByNameAsync(titleName, It.IsAny<CancellationToken>()), Times.Once);
        _titleRepo.Verify(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}
