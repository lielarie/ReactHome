using Microsoft.EntityFrameworkCore;
using ReactHome.Server.Infrastructure;
using ReactHome.Server.Models;

namespace ReactHome.Server.Helpers;

public sealed class TagsResolver
{
    private readonly AppDbContext _db;

    public TagsResolver(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Tag>> LoadTags(
        IEnumerable<Guid>? tagIds,
        CancellationToken ct = default)
    {
        if (tagIds is null)
            return [];

        var ids = tagIds
            .Where(id => id != Guid.Empty)
            .Distinct()
            .ToList();

        if (ids.Count == 0)
            return new List<Tag>();

        return await _db.Tags
            .Where(t => ids.Contains(t.Id))
            .ToListAsync(ct);
    }
}
