using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using To_Do_API.Data.DBContext;
using To_Do_API.Data.Models;

namespace To_Do_API.Services
{
    public class FetchToDoService
    {
        private readonly ToDoContext _context;

        public FetchToDoService(ToDoContext dbContext)
        {
            _context = dbContext;
        }

        public async Task FetchAndStoreTodosAsync()
        {
            using var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync("https://dummyjson.com/todos");

                // Parse the JSON response to the list of Todos
                var todosData = JObject.Parse(response)["todos"];
                var todos = todosData
                    .Select(todo => new
                    {
                        Title = todo["todo"]?.ToString() ?? string.Empty, // Accessing todo property
                        Completed = todo["completed"]?.Value<bool>() ?? false, // Accessing completed property
                        UserId = todo["userId"]?.Value<int>() ?? 0 // Accessing userId property
                    }).ToList();

                // Now map to ToDo model
                var todoEntities = todos.Select(todo => new ToDo
                {
                    Id = 0, // Ensure the ID is not set for insertion
                    Title = todo.Title, // Map to Title
                    Completed = todo.Completed,
                    UserId = todo.UserId
                }).ToList();

                // Add the todo entities to the context
                await _context.ToDos.AddRangeAsync(todoEntities);
                int savedCount = await _context.SaveChangesAsync();
                Console.WriteLine($"Number of todos saved: {savedCount}");

                //await _context.SaveChangesAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle any errors that occurred during the HTTP request
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                // Handle any errors that occurred during the database update
                Console.WriteLine($"Database update error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
