using Task = API.Models.Task;

namespace API.Services.Interfaces;

public interface ITask
{
    Task<List<Task>> GetAllTask();
    Task<Task> GetTaskById(string id);
    Task<Task> CreateTask(Task task);
    Task<bool> UpdateTask(string id, Task updateTask);
    Task<bool> DeleteTask(string id);
}