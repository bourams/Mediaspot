using Mediaspot.Application.Common;
using Mediaspot.Domain.Transcoding;
using System.Threading.Channels;

namespace Mediaspot.Infrastructure.Queue;

public sealed class MediaSpotQueue : IMessageQueue
{
    private readonly Channel<TranscodeJob> _channel;

    public MediaSpotQueue()
    {
        _channel = Channel.CreateUnbounded<TranscodeJob>(new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = false
        });
    }

    public Task EnqueueAsync(TranscodeJob job, CancellationToken ct) => 
         _channel.Writer.WriteAsync(job, ct).AsTask();

    public Task<TranscodeJob> DequeueAsync(CancellationToken ct) => 
         _channel.Reader.ReadAsync(ct).AsTask();

    public Task<bool> Watch(CancellationToken ct) => 
        _channel.Reader.WaitToReadAsync(ct).AsTask();
}