using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;

namespace Mediaspot.Worker.Transcode;

//public sealed class TranscorderWorker : BackgroundService
//{
//    // Vraiment vraiment vraiment pas sur de ca
//    private readonly IPublisher _publisher;
//    private readonly IMessageQueue _queue;

//    public TranscorderWorker(IPublisher publisher, IMessageQueue queue)
//    {
//        _queue = queue;
//        _publisher = publisher;
//    }

//    protected override async Task ExecuteAsync(CancellationToken ct)
//    {
//        while (await _queue.Watch(ct))
//        {
//            var job = await _queue.DequeueAsync(ct);

//            try
//            {
//                job.MarkRunning();
//                // Vraiment vraiment vraiment pas sur de ca
//                await _publisher.Publish(new TranscodeStateChanged(job.Id, job.Status));

//                await Task.Delay(1000);

//                job.MarkSucceeded();
//                await _publisher.Publish(new TranscodeStateChanged(job.Id, job.Status));
//            }
//            catch (Exception)
//            {
//                job.MarkFailed();
//                await _publisher.Publish(new TranscodeStateChanged(job.Id, job.Status));
//                throw;
//            }

//        }
//    }
//}

public sealed class TranscorderWorker : BackgroundService
{
    // Vraiment vraiment vraiment pas sur de ca
    private readonly IMessageQueue _queue;
    private readonly ITranscodeJobRepository _repo;
    private readonly IUnitOfWork _uow;

    public TranscorderWorker(IServiceScopeFactory scopeFactory, IMessageQueue queue)
    {
        _queue = queue;
        using var scope = scopeFactory.CreateScope();
        _repo = scope.ServiceProvider.GetRequiredService<ITranscodeJobRepository>();
        _uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (await _queue.Watch(ct))
        {
            var job = await _queue.DequeueAsync(ct);

            try
            {
                job.MarkRunning();
                
                await Task.Delay(1000);

                job.MarkSucceeded();
            }
            catch (Exception)
            {
                job.MarkFailed();
                throw;
            }

        }
    }
}