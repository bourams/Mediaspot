using Mediaspot.Application.Common;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Update;

public class UpdateTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpdateTitleCommand>
{
    public async Task Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
    {
        var title = await repo.GetAsync(request.TitleId, cancellationToken) ?? throw new KeyNotFoundException("Title not found");
        var existing = await repo.ExistsByNameAsync(request.Name, cancellationToken);
        if (existing)
            throw new InvalidOperationException($"Title with name '{request.Name}' already exists.");

        title.Update(request.Name, request.Description, request.ReleaseDate, request.Type);

        await uow.SaveChangesAsync(cancellationToken);
    }
}
