using System.Net.Http.Json;
using MudBlazor;
using Task.Client.DTO;
using Task.Client.Interface;

namespace Task.Client.Services;

public class TaskService : ITask
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Models.Task>?> GetAllTask()
    {
        return await _httpClient.GetFromJsonAsync<List<Models.Task>>("api/Task/GetAllTask");
    }

    public async Task<Models.Task?> Createtask(Models.Task task)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Task/CreateTask", task);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Models.Task>();
    }

    // public async Task<Models.Task> UpdateTask(UpdateDto taskDto)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<bool> DeleteTask(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/Task/{id}");
        return response.IsSuccessStatusCode;
    }
}