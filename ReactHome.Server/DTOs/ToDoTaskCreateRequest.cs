using ReactHome.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace ReactHome.Server.DTOs;

public sealed class ToDoTaskCreateRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(50)]
    public string Title { get; set; }

    [Required, MaxLength(200)]
    public string Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public Priority Priority { get; set; }

    public List<Guid> TagIds { get; set; } = [];
}
