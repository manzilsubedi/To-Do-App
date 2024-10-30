# To-Do Backend API

This project is a C# REST API for managing a To-Do list, including integration with external APIs for additional features. The backend retrieves To-Do items from an external source, adds custom fields for each task, supports CRUD operations, and integrates with a weather service to provide current conditions for tasks with locations specified.

## Features

- Fetches initial To-Do data from [DummyJSON](https://dummyjson.com/todos) and stores it locally.
- Includes custom fields for each To-Do item:
  - **Category**: Optional relationship to a predefined Category List.
  - **Priority**: Ranges from 1 (highest priority) to 5 (lowest), default is 3.
  - **Location**: Optional, includes latitude and longitude.
  - **Due Date**: Optional, stores DateTime.

- **Category List** includes:
  - **Title**: Category name.
  - **Parent Category**: Optional, allows nested categories.

- Provides a new REST API supporting:
  - CRUD operations for To-Do items.
  - Search functionality by Title, Priority, or Due Date.
  - Integration with [WeatherAPI](https://www.weatherapi.com/) to retrieve current temperature and conditions if Location is set.

## API Endpoints

- **POST /todos**: Add a new To-Do item.
- **GET /todos**: Retrieve a list of To-Do items.
- **GET /todos/{id}**: Retrieve a single To-Do item by ID.
- **PUT /todos/{id}**: Update an existing To-Do item.
- **DELETE /todos/{id}**: Delete a To-Do item by ID.
- **GET /todos/search**: Search To-Do items by Title, Priority, or Due Date.
- **GET /todos/{id}/weather**: Retrieves current weather data if Location is set on the To-Do item.

## Installation

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup
1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/todo-backend-api.git
   cd todo-backend-api
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Set up the database**:
   - Update the `appsettings.json` file with your SQL Server connection string.
   - Run migrations to set up the local SQL database:
     ```bash
     dotnet ef database update
     ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

### Running the Application
The API will start on `http://localhost:5000` (or another port if specified in `launchSettings.json`).

## Usage

1. **Fetching To-Do List**: The API initially fetches To-Do data from [DummyJSON](https://dummyjson.com/todos) and stores it locally, including custom fields like Category, Priority, Location, and Due Date.
2. **Weather Integration**: If a To-Do item has a location specified, you can retrieve its current weather conditions (temperature and status) using the `/todos/{id}/weather` endpoint.

## Forking and Contributing

To fork and contribute:

1. Fork this repository by clicking the "Fork" button at the top.
2. Clone your forked repository to your machine.
3. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b your-feature-branch
   ```
4. Push to your forked repository and submit a Pull Request.

## License
This project is licensed under the MIT License.

---
