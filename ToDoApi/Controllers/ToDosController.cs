using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoLibrary.DataAccess;
using ToDoLibrary.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDosController : ControllerBase
{
    private readonly IToDoData _data;
    private readonly ILogger<ToDosController> _logger;

    public ToDosController(IToDoData data, ILogger<ToDosController> logger)
    {
        _data = data;
        _logger = logger;
    }

    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdText);
    }

    // GET: api/ToDos
    [HttpGet]
    public async Task<ActionResult<List<ToDoModel>>> Get()
    {
        _logger.LogInformation("GET: api/Todos");
        try
        {
            var output = await _data.GetAllAssigned(GetUserId());
            
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GET call to api/Todos failed");
            return BadRequest();
        }

       
    }

    // GET api/ToDos/5
    [HttpGet("{todoId}")]
    public async Task<ActionResult<ToDoModel>> Get(int todoId)
    {
        _logger.LogInformation("GET: api/Todos/{TodoId}", todoId);

        try
        {
            var output = await _data.GetOneAssigned(GetUserId(), todoId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GET call to {ApiPath} failed. The Id was {TodoId}.", $"api/Todos/Id", todoId);
            return BadRequest();
        }
    }
   
    // POST api/ToDos
    [HttpPost]
    public async Task<ActionResult<ToDoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation("POST: api/Todos (Task: {Task})", task);

        try
        {
            var output = await _data.Create(GetUserId(), task);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "POST call to {ApiPath} failed. The Task was {Task}.", $"api/Todos", task);
            return BadRequest();
        }
    }

    // PUT api/ToDos/5
    [HttpPut("{todoId}")]
    public async Task<ActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation("PUT: api/Todos/Id (Id: {Id}) (Task: {Task})", todoId, task);

        try
        {
            await _data.UpdateTask(GetUserId(), todoId, task);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PUT call to {ApiPath} failed. The Id was {TodoId}. The Task was {Task}.", $"api/Todos/Id", todoId, task);
            return BadRequest();
        }
    }

    // PUT api/ToDos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public async Task<IActionResult> Complete(int todoId)
    {
        _logger.LogInformation("PUT: api/Todos/Id/Complete (Id: {Id})", todoId);

        try
        {
            await _data.CompleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PUT call to {ApiPath} failed. The Id was {TodoId}.", $"api/Todos/Id/Complete", todoId);
            return BadRequest();
        }
    }

    // DELETE api/ToDos/5
    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation("DELETE: api/Todos/Id/Complete (Id: {Id})", todoId);

        try
        {
            await _data.DeleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DELETE call to {ApiPath} failed. The Id was {TodoId}.", $"api/Todos/Id", todoId);
            return BadRequest();
        }
    }
}
