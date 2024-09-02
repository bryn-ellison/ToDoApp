using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess
{
    public interface IToDoData
    {
        Task CompleteTodo(int assignedTo, int todoId);
        Task<ToDoModel?> Create(int assignedTo, string task);
        Task DeleteTodo(int assignedTo, int todoId);
        Task<List<ToDoModel>> GetAllAssigned(int assignedTo);
        Task<ToDoModel?> GetOneAssigned(int assignedTo, int todoId);
        Task UpdateTask(int assignedTo, int todoId, string task);
    }
}