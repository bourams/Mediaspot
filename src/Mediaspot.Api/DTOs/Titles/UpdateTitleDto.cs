using Mediaspot.Domain.Titles;

namespace Mediaspot.Api.DTOs.Titles;

public sealed record UpdateTitleDto(string Name, string? Description, DateOnly? releaseDate, TitleType Type);

