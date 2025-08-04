using Mediaspot.Domain.Titles;

namespace Mediaspot.Api.DTOs.Titles;

public sealed record TitleDto(string Name, string? Description, DateOnly? ReleaseDate, TitleType title);
