using Mediaspot.Domain.Common;
using Mediaspot.Domain.Titles.Events;

namespace Mediaspot.Domain.Titles;

public enum TitleType { Movie = 1, Serie = 2 }

public sealed class Title : AggregateRoot
{
    public string Name { get; init; }

    public string? Description { get; init; }

    public DateOnly? ReleaseDate { get; init; }

    public TitleType Type { get; init; }

    public Title(string name, string? description, DateOnly? releaseDate, TitleType type)
    {
        Name = name;
        Description = description;
        ReleaseDate = releaseDate;
        Type = type;
        Raise(new TitleCreated(Id));
    }
}

