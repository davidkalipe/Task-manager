using Task.Client.DTO;

namespace Task.Client.Interface;

public interface ITask
{
    Task<List<Models.Task>> GetAllTask();
    Task<Models.Task> Createtask(Models.Task task);
    Task<Models.Task> UpdateTask(UpdateDto taskDto);
    Task<Models.Task> DeleteTask(string id);
}