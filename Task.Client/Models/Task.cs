namespace Task.Client.Models;

public class Task
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favorite { get; set; }
}