namespace To_Do_API.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
