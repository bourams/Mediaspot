namespace Mediaspot.Api.DTOs;

public sealed record TranscodeRequestDto(Guid MediaFileId, string Preset);

