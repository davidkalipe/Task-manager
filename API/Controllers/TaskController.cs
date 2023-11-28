using API.DTO;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task = API.Models.Task;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(TaskService taskService, IMapper mapper)
    {
        _mapper = mapper;
        _taskService = taskService;
    }

    [HttpGet("GetAllTask")]
    public async Task<OkObjectResult> GetAllTask()
    {
        var lestask = await _taskService.GetAllTask();
        return Ok(lestask);
    }

    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask(CreateDto taskDto)
    {
        try
        {
            var letask = _mapper.Map<Task>(taskDto);
            var newTask = await _taskService.CreateTask(letask);

            if (newTask == null)
                return BadRequest("Problème lors de la création de la tâche");
            var task = _mapper.Map<CreateDto>(letask);
            return Ok(task);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erreur de serveur");
        }
    }

    [HttpPut("UpdateTask")]
    public async Task<IActionResult> UpdateTask(UpdateDto updateDto)
    {
        try
        {
            var leTask = _mapper.Map<Task>(updateDto);
            var existingTask = await _taskService.UpdateTask(leTask);
            if (existingTask)
                return Ok("Task update successfully");
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(statusCode: 500, value: "Erreur au niveau du serveur");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var existingTask = await _taskService.DeleteTask(id);
        if (!existingTask)
            return NotFound("Tâche introuvable");
        return Ok("Tâche supprimée avec succès");
    }
}