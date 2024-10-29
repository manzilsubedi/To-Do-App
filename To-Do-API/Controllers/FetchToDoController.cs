using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_API.Services;

namespace To_Do_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchToDoController : ControllerBase
    {
        private readonly FetchToDoService _fetchToDoService;

        public FetchToDoController(FetchToDoService fetchToDoService)
        {
            _fetchToDoService = fetchToDoService;
        }

        // Endpoint to trigger fetching and storing todos
        [HttpPost("fetch-and-store")]
        public async Task<IActionResult> FetchAndStoreTodos()
        {
            await _fetchToDoService.FetchAndStoreTodosAsync();
            return Ok("To-Do items fetched from dummy data and stored successfully.");
        }
    }
}
