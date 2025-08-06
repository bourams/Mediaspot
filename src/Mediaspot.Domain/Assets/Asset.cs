using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Assets;

public sealed class Asset : AggregateRoot
{
    private readonly List<MediaFile> _mediaFiles = [];

    public string ExternalId { get; private set; }
    public string TitleId { get; private set; }
    public Metadata Metadata { get; private set; }
    public bool Archived { get; private set; }

    public IReadOnlyCollection<MediaFile> MediaFiles => _mediaFiles.AsReadOnly();

    private Asset() { ExternalId = string.Empty; TitleId = string.Empty; Metadata = new("", null, null); }

    public Asset(string externalId, string titleId, Metadata metadata)
    {
        ExternalId = externalId;
        TitleId = titleId;
        Metadata = metadata;
        Raise(new AssetCreated(Id));
    }

    public MediaFile RegisterMediaFile(FilePath path, Duration duration)
    {
        var mf = new MediaFile(MediaFileId.New(), path, duration);
        _mediaFiles.Add(mf);
        Raise(new MediaFileRegistered(Id, mf.Id.Value));
        return mf;
    }

    public void UpdateMetadata(Metadata metadata)
    {
        // invariant: cannot set empty title
        if (string.IsNullOrWhiteSpace(metadata.Title))
            throw new ArgumentException("Title is required", nameof(metadata));

        Metadata = metadata;
        Raise(new MetadataUpdated(Id));
    }

    public void Archive(Func<Guid, bool> hasActiveJobs)
    {
        if (Archived) return;
        if (hasActiveJobs(Id))
            throw new InvalidOperationException("Cannot archive while active transcode jobs exist.");

        Archived = true;
        Raise(new AssetArchived(Id));
    }

    public void Transcode(Guid mediaFileId, string preset)
    {
        var mediaFile = MediaFiles.FirstOrDefault(x => x.Id.Value == mediaFileId);
        if (mediaFile is null)
            throw new KeyNotFoundException("Mediafile not found");

        Raise(new TranscodeRequested(Id, mediaFileId, preset));
    }
}
