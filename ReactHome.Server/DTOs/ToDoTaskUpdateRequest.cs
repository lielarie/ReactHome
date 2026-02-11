using ReactHome.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace ReactHome.Server.DTOs;


public sealed class ToDoTaskUpdateRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public Priority? Priority { get; set; }

    public Guid UserId { get; set; }

    public List<Guid>? TagIds { get; set; }
}
