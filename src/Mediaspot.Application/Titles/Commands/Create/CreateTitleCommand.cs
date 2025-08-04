using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed record CreateTitleCommand(string Name, string? Description, DateOnly? ReleaseDate, TitleType Type) : IRequest<Guid>;

