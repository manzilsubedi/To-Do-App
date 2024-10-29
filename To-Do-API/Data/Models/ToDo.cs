using System.Text.Json.Serialization;

namespace To_Do_API.Data.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; } // Corresponds to "todo" in JSON
        public bool Completed { get; set; } // Corresponds to "completed" in JSON
        public int UserId { get; set; }
        public int Priority { get; set; } = 3; // Default to 3
        public DateTime? DueDate { get; set; }
        public string Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
