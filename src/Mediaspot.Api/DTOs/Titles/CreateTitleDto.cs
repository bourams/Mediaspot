using Mediaspot.Domain.Titles;

namespace Mediaspot.Api.DTOs.Titles;

public sealed record CreateTitleDto(string Name, string? Description, DateOnly? ReleaseDate, TitleType Type);
