using Mediaspot.Domain.Common;
using Mediaspot.Domain.Titles.Events;

namespace Mediaspot.Domain.Titles;

public enum TitleType { Movie = 1, Serie = 2 }

public sealed class Title : AggregateRoot
{
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public DateOnly? ReleaseDate { get; private set; }

    public TitleType Type { get; private set; }

    public Title(string name, string? description, DateOnly? releaseDate, TitleType type)
    {
        Name = name;
        Description = description;
        ReleaseDate = releaseDate;
        Type = type;
        Raise(new TitleCreated(Id));
    }

    public void Update(string name, string? description, DateOnly? releaseDate, TitleType type)
    {
        Name = name;
        Description = description;
        ReleaseDate = releaseDate;
        Type = type;

        Raise(new TitleUpdated(Id));
    }
}

