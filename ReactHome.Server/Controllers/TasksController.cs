using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactHome.Server.DTOs;
using ReactHome.Server.Helpers;
using ReactHome.Server.Infrastructure;
using ReactHome.Server.Models;
using System.Reflection;

namespace ReactHome.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(AppDbContext db, TagsResolver tagResolver) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoTask>> GetTaskById(Guid id, CancellationToken ct)
    {
        var task = await db.Tasks
            .AsNoTracking()
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, ct);

        if (task is null) return NotFound();

        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<List<ToDoTask>>> GetAllUserTasks(
        [FromQuery] string userEmail,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(userEmail))
            return BadRequest("User email is required.");

        var tasks = await db.Tasks
            .Include(t => t.Tags)
            .Where(t => t.User.Email == userEmail)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync(ct);

        return Ok(tasks.Select(t => new ToDoTaskResponse
        {
            Id = t.Id,
            UserId = t.UserId,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Priority = t.Priority,
            Tags = t.Tags.Select(x => new Tag { Id = x.Id, Name = x.Name }).ToList()
        }));
    }

    [HttpPost]
    public async Task<ActionResult<ToDoTaskResponse>> CreateTask([FromBody] ToDoTaskCreateRequest req, CancellationToken ct)
    {
        var tags = await tagResolver.LoadTags(req.TagIds, ct);

        var entity = new ToDoTask
        {
            UserId = req.UserId,
            Title = req.Title,
            Description = req.Description,
            DueDate = req.DueDate,
            Priority = req.Priority,
            Tags = tags.Select(t => new Tag { Id = t.Id, Name = t.Name }).ToList()
        };

        db.Tasks.Add(entity);
        await db.SaveChangesAsync(ct);

        return StatusCode(StatusCodes.Status201Created, entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Task>> UpdateTask(Guid id, [FromBody] ToDoTaskUpdateRequest req, CancellationToken ct)
    {
        var entity = await db.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, ct);

        if (entity is null) return NotFound();

        if (req.Title is not null)
            entity.Title = req.Title;

        if (req.Description is not null)
            entity.Description = req.Description;

        if (req.DueDate is not null)
            entity.DueDate = (DateTime)req.DueDate;

        if (req.Priority is not null)
            entity.Priority = (Priority)req.Priority;

        if (req.UserId != default)
            entity.UserId = req.UserId;

        if (req.TagIds != default)
        {
            var tags = await tagResolver.LoadTags(req.TagIds, ct);

            entity.Tags.Clear();

            foreach (var tag in tags)
                entity.Tags.Add(tag);
        }

        await db.SaveChangesAsync(ct);

        return Ok($"Successfully updated task {entity.Id}.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id, CancellationToken ct)
    {
        var entity = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
        if (entity is null) return NotFound();

        db.Tasks.Remove(entity);
        await db.SaveChangesAsync(ct);

        return Ok($"Successfully deleted task: {id}");
    }
}
