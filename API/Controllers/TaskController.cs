using API.Data;
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
    private readonly IMapper _mapper;
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService, IMapper mapper)
    {
        _mapper = mapper;
        _taskService = taskService;
    }
    

    [HttpGet("GetAllTask")]
    public async Task<ActionResult<List<Task>>> GetAllTask()
    {
        var lestask = await _taskService.GetAllTask();
        var taskdto = _mapper.Map<List<TaskDto>>(lestask);
        return Ok(taskdto);
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
            return Ok("Task update successfully");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erreur au niveau du serveur");
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