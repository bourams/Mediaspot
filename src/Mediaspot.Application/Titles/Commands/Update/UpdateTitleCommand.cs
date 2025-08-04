using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Update;

public sealed record UpdateTitleCommand(Guid TitleId, string Name, string? Description, DateOnly? ReleaseDate, TitleType Type) : IRequest;

