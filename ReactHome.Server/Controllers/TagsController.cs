using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactHome.Server.DTOs;
using ReactHome.Server.Infrastructure;
using ReactHome.Server.Models;

namespace ReactHome.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController(AppDbContext db) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetAllTags(CancellationToken ct)
        {
            var tags = await db.Tags
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .Select(t => new Tag { Id = t.Id, Name = t.Name })
                .ToListAsync(ct);

            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(TagRequest tag, CancellationToken ct)
        {
            var normalizedName = tag.Name.Trim().ToLowerInvariant();

            var tagExists = await db.Tags.AnyAsync(u => u.Name.ToLower() == normalizedName, ct);

            if (tagExists)
                return Conflict("Tag with this name already exists.");

            var entity = new Tag
            {
                Name = tag.Name.Trim(),
            };

            db.Tags.Add(entity);
            await db.SaveChangesAsync(ct);

            return Ok($"Successfully created new tag: {entity.Id}");
        }
    }
}
