using Microsoft.AspNetCore.Mvc;
using Moq;
using To_Do_API.Controllers;
using To_Do_API.Data.Models;
using To_Do_API.Services;
using To_Do_API.Services.Interfaces;

namespace UnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddTodo_ShouldReturnCreatedTodoWithId()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();          // Mock the ToDoService
            var mockWeatherService = new Mock<IWeatherService>();     // Mock the WeatherService

            var newTodo = new ToDo { Title = "New To-Do", Priority = 3 };
            var createdTodo = new ToDo { Id = 1, Title = "New To-Do", Priority = 3 };

            // Set up the ToDoService mock to return createdTodo when AddTodoItemAsync is called
            mockToDoService.Setup(service => service.AddTodoItemAsync(newTodo)).ReturnsAsync(createdTodo);

            // Initialize the controller with mocked services
            var controller = new ToDoController(mockToDoService.Object, mockWeatherService.Object);

            // Act
            var result = await controller.AddTodoItem(newTodo);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);  // Check if result is CreatedAtActionResult
            var returnValue = Assert.IsType<ToDo>(createdResult.Value);        // Check if result's value is of type ToDo
            Assert.Equal("New To-Do", returnValue.Title);                      // Verify title
            Assert.Equal(1, returnValue.Id);

        }

        [Fact]
        public async Task GetTodoList_ShouldReturnOkResulOfTodos()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var mockWeatherService = new Mock<IWeatherService>();

            var todoItem = new ToDo { Id = 1, Title = "Test Todo", Priority = 3 };

            // Set up the mock to return the todoList when GetTodoListAsync is called
            mockToDoService.Setup(service => service.GetTodoItemAsync(1)).ReturnsAsync(todoItem);

            // Initialize the controller with mocked services
            var controller = new ToDoController(mockToDoService.Object, mockWeatherService.Object);

            // Act
            var result = await controller.GetTodoItem(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ToDo>>(result);  // Check if result is ActionResult<ToDo>
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result); // Check if the inner result is OkObjectResult
            var returnValue = Assert.IsType<ToDo>(okResult.Value); // Ensure the returned value is of type ToDo
            Assert.Equal("Test Todo", returnValue.Title);   // Verify the count of returned ToDo items
        }


    }
}