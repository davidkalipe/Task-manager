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

    [HttpPut("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(string id, UpdateDto updateDto)
    {
        try
        {
            var existingTask = await _taskService.GetTaskById(id);
            if (existingTask == null)
                return NotFound("Tâche introuvable");

            var letask = _mapper.Map<Task>(updateDto);
            var updateTask = await _taskService.UpdateTask(id, letask);
            if (!updateTask)
                return BadRequest("Mise à jour échoué");
            return Ok(updateTask);
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