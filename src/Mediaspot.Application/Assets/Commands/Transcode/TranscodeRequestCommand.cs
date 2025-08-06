using MediatR;

namespace Mediaspot.Application.Assets.Commands.Transcode;

public sealed record TranscodeRequestCommand(Guid AssetId, Guid MediaFileId, string Preset) : IRequest;
 