using System.Data.Common;
using API.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Task = API.Models.Task;


namespace API_Test;

internal class TaskContextMock
{
    private DbConnection _connexion;
    private DbContextOptions<TaskDbContext> _contextOptions;
    public TaskDbContext Context { get; }
    
    public TaskContextMock()
    {
        _connexion = new SqliteConnection("Filename=:memory:");
        _connexion.Open();
        _contextOptions = new DbContextOptionsBuilder<TaskDbContext>().UseSqlite(_connexion).Options;

        Context = new TaskDbContext(_contextOptions);
        Context.Database.EnsureCreated();

     
        Task task1 = new Task
            { Id = "poiuy", Name = "Manger", Description = "Dans laprem", Favorite = true };
        Task task2 = new Task
            { Id = "ftgyuhb", Name = "Dormir", Description = "Tôt là 22h", Favorite = true };

        Context.Tasks.Add(task1);
        Context.Tasks.Add(task2);
        Context.SaveChanges();
    }

    public void Dispose() => _connexion.Dispose();
}