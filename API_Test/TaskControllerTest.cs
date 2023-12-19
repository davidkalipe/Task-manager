using API.Controllers;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using Task = API.Models.Task;

namespace API_Test;

public class TaskControllerTest
{ 
    private TaskContextMock _contextMock = new TaskContextMock();
    public void Dispose() => _contextMock.Dispose();

    [Fact]
    public async void GetTask_All_GetAll()
    {
        //Arrange
        TaskService service = new TaskService(_contextMock.Context);
        
        //Act
        var result = await service.GetAllTask();
        
        //Assert
        var returnValue = Assert.IsType<List<Task>>(result);
        Assert.Equal(_contextMock.Context.Tasks.Count(), returnValue.Count);

    }

    [Fact]
    public async void CreateTask_Verify_Task_Creation()
    {
        TaskService service = new TaskService(_contextMock.Context);
        Task task = new Task();

        var result = await service.CreateTask(task);

        var actionResult = Assert.IsType<Task>(result);
        Assert.Equal(task.Id, actionResult.Id);
    }
}