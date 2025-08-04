using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Commands;

public sealed class CreateTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateTitleCommand, Guid>
{
    public async Task<Guid> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
    {
        var existing = await repo.ExistsByNameAsync(request.Name, cancellationToken);
        if (existing)
        {
            // TODO: Exception Custom
            throw new InvalidOperationException($"Title with name '{request.Name}' already exists.");
        }

        var title = new Title(request.Name, request.Description, request.ReleaseDate, request.Type);
        await repo.AddAsync(title, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return title.Id;
    }
}

