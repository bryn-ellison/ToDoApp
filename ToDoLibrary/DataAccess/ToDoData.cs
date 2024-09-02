using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess;

public class ToDoData : IToDoData
{
    private readonly ISqlDataAccess _sql;

    public ToDoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public Task<List<ToDoModel>> GetAllAssigned(int assignedTo)
    {
        return _sql.LoadData<ToDoModel, dynamic>("dbo.spTodos_GetAllAssigned", new { AssignedTo = assignedTo }, "Default");
    }

    public async Task<ToDoModel?> GetOneAssigned(int assignedTo, int todoId)
    {
        var results = await _sql.LoadData<ToDoModel, dynamic>("dbo.spTodos_GetOneAssigned",
                                                              new { AssignedTo = assignedTo, TodoId = todoId }, "Default");

        return results.FirstOrDefault();
    }

    public async Task<ToDoModel?> Create(int assignedTo, string task)
    {
        var results = await _sql.LoadData<ToDoModel, dynamic>("dbo.spTodos_Create",
                                                              new { AssignedTo = assignedTo, Task = task }, "Default");

        return results.FirstOrDefault();
    }

    public Task UpdateTask(int assignedTo, int todoId, string task)
    {
        return _sql.SaveData<dynamic>("dbo.spTodos_UpdateTask", new { AssignedTo = assignedTo, TodoId = todoId, Task = task }, "Default");
    }

    public Task CompleteTodo(int assignedTo, int todoId)
    {
        return _sql.SaveData<dynamic>("dbo.spTodos_CompleteTodo", new { AssignedTo = assignedTo, TodoId = todoId }, "Default");
    }

    public Task DeleteTodo(int assignedTo, int todoId)
    {
        return _sql.SaveData<dynamic>("dbo.spTodos_Delete", new { AssignedTo = assignedTo, TodoId = todoId }, "Default");
    }
}
