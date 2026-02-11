namespace ReactHome.Server.Models;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public List<ToDoTask> Tasks { get; set; } = new();
}
