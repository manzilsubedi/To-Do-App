using To_Do_API.Data.Models;

namespace To_Do_API.Services.Interfaces
{
    public interface IToDoService
    {
        Task<ToDo> AddTodoItemAsync(ToDo todo);
        Task<object?> GetTodoItemAsync(int id);
        Task UpdateTodoItemAsync(ToDo todo);
        Task<bool> DeleteTodoItemAsync(int id);

    }
}