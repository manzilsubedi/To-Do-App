using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using To_Do_API.Data.Models;
using To_Do_API.Services;

namespace To_Do_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDoService;
        private readonly WeatherService _weatherService;

        public ToDoController(ToDoService toDoService, WeatherService weatherService)
        {
            _toDoService = toDoService;
            _weatherService = weatherService;
        }

        [HttpGet("weather/{location}")]
        public async Task<IActionResult> GetWeatherByLocation(string location)
        {
            try
            {
                var weatherInfo = await _weatherService.GetWeatherAsync(location);
                return Ok(weatherInfo);
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoItem(ToDo todo)
        {
            var createdTodo = await _toDoService.AddTodoItemAsync(todo);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetTodoItem(int id)
        {
            var todo = await _toDoService.GetTodoItemAsync(id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, ToDo todo)
        {
            if (id != todo.Id) return BadRequest();

            await _toDoService.UpdateTodoItemAsync(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var deleted = await _toDoService.DeleteTodoItemAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
