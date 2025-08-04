using Mediaspot.Domain.Titles;

namespace Mediaspot.Application.Common;

public interface ITitleRepository
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
    Task AddAsync(Title title, CancellationToken ct);
    Task<Title?> GetAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Title>> ListAsync(int page, int pageSize, CancellationToken ct);
}
