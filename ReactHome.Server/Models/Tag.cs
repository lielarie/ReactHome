using Newtonsoft.Json;

namespace ReactHome.Server.Models;

public class Tag
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<ToDoTask>? Tasks { get; set; }
}