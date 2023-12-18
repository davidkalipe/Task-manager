namespace API.Models;

public class Task
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favorite { get; set; }

    
    public Task()
    {
        Id = GenerateTimestampId();
    }
    

    private string GenerateTimestampId()
    {
        DateTime now = DateTime.Now;
        string timestamp = now.ToString("yyyyMMddHHmmss");
        return timestamp;
    }
}