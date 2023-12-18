using API.Controllers;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
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
        TaskController controller = new TaskController(_contextMock.Context);
        
        //Act
        var result = await controller.GetAllTask();
        
        //Assert
        var actionResult = Assert.IsType<ActionResult<List<Task>>>(result);
        var returnValue = Assert.IsType<List<Task>>(actionResult.Value);
        Assert.Equal(_contextMock.Context.Tasks.Count(), returnValue.Count);

    }
}