using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using To_Do_API.Data;
using To_Do_API.Data.DBContext;
using To_Do_API.Data.Models;
using To_Do_API.Services.Interfaces;

namespace To_Do_API.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoContext _context;
        private readonly WeatherService _weatherService;

        public ToDoService(ToDoContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        public async Task<ToDo> AddTodoItemAsync(ToDo todo)
        {
            var weatherInfo = await _weatherService.GetWeatherAsync(todo.Location);
            var todoEntity = new ToDo
            {
                Title = todo.Title,
                Completed = todo.Completed,
                UserId = todo.UserId,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                Location = todo.Location, // Only set the location from input
                Latitude = weatherInfo.Latitude, // Set latitude from weather info
                Longitude = weatherInfo.Longitude // Set longitude from weather info
            };
            _context.ToDos.Add(todoEntity);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<object?> GetTodoItemAsync(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);

            if (todo == null)
            {
                return null; // Return null instead of NotFound()
            }

            // Fetch weather info if Location is set
            WeatherInfo? weatherInfo = null;
            if (!string.IsNullOrEmpty(todo.Location))
            {
                weatherInfo = await _weatherService.GetWeatherAsync(todo.Location);
            }

            var todoWithWeather = new
            {
                todo.Id,
                todo.Title,
                todo.Completed,
                todo.UserId,
                todo.Priority,
                todo.DueDate,
                todo.Location,
                todo.Latitude,
                todo.Longitude,
                Weather = weatherInfo // Include weather data in the response
            };

            return todoWithWeather; // Return the combined object
        }


        public async Task UpdateTodoItemAsync(ToDo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null) return false;

            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
