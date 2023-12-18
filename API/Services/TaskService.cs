using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = API.Models.Task;

namespace API.Services;

public class TaskService : ITask
{
    private readonly TaskDbContext _dbContext;

    public TaskService(TaskDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<List<Task>> GetAllTask()
    {
        var lestask = await _dbContext.Tasks.ToListAsync();
        return lestask;
    }

    public async Task<Task?> GetTaskById(string id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task == null)
            return null;
        return task;
    }
    
    public async Task<Task> CreateTask(Task task)
    {
        var letask = await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        return letask.Entity;
    }
    
    public async Task<bool> UpdateTask(Task task)
    {
        var existantTask = await _dbContext.Tasks.FirstOrDefaultAsync(t=>t.Id == task.Id);
        if (existantTask != null)
        {
            existantTask.Name = task.Name;
            existantTask.Description = task.Description;
            existantTask.Favorite = task.Favorite;
            
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    public async Task<bool> DeleteTask(string id)
    {
        var existingTask = _dbContext.Tasks.FirstOrDefault(t=>t.Id == id);
        if (existingTask == null)
            return false;

        _dbContext.Tasks.Remove(existingTask);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}