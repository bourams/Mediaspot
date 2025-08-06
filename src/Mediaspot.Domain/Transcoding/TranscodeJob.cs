using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Transcoding;

public enum TranscodeStatus { Pending, Running, Succeeded, Failed }

public sealed class TranscodeJob : AggregateRoot
{
    public Guid AssetId { get; private set; }
    public Guid MediaFileId { get; private set; }
    public string Preset { get; private set; }
    public TranscodeStatus Status { get; private set; }
    public DateTime CratedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private TranscodeJob() { AssetId = Guid.Empty; MediaFileId = Guid.Empty; Preset = string.Empty; }

    public TranscodeJob(Guid assetId, Guid mediaFileId, string preset)
    {
        AssetId = assetId;
        MediaFileId = mediaFileId;
        Preset = preset; 
        Status = TranscodeStatus.Pending;
        CratedAt = DateTime.UtcNow;
        // TODO: Raise 
    }

    public void MarkRunning() => Status = TranscodeStatus.Running;

    //public void MarkRunning()
    //{
    //    if (Status != TranscodeStatus.Pending)
    //        exception

    //    Status = TranscodeStatus.Running;
    //    UpdatedAt = DateTime.UtcNow;
    //    Raise(new TranscodeJobStarted(Id));
    //}

    public void MarkSucceeded() => Status = TranscodeStatus.Succeeded;
    public void MarkFailed() => Status = TranscodeStatus.Failed;
}