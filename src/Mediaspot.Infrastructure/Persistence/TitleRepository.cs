using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;
public sealed class TitleRepository(MediaspotDbContext db) : ITitleRepository
{
    public async Task AddAsync(Title title, CancellationToken ct) =>
        await db.Titles.AddAsync(title, ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) => 
        await db.Titles.AnyAsync(x => x.Name == name, ct);

    public async Task<Title?> GetAsync(Guid id, CancellationToken ct) =>
        await db.Titles.SingleOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IEnumerable<Title>> ListAsync(int page, int pageSize, CancellationToken ct)
    {
        return await db.Titles.OrderBy(t => t.Name)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync(ct);
    }
}
