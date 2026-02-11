using ReactHome.Server.Models;

namespace ReactHome.Server.DTOs;

public sealed class ToDoTaskResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public Priority Priority { get; set; }

    public List<Tag> Tags { get; set; }
}
