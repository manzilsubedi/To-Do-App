using Microsoft.EntityFrameworkCore;
using To_Do_API.Data.Models;

namespace To_Do_API.Data.DBContext
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().ToTable("ToDos");
        }
    }
}
