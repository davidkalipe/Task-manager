using API.DTO;
using Task = API.Models.Task;

namespace API.Services.Interfaces;

public interface ITask
{
    Task<List<Task>> GetAllTask();
    Task<Task?> GetTaskById(string id);
    Task<Task> CreateTask(Task task);
    Task<Task> UpdateTask(Task task);
    Task<bool> DeleteTask(string id);
}