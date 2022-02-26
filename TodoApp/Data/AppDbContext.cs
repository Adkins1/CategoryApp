using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
        // db context dostaje w parametrach opcje które są przekazywane do klasy bazowej (dbcontext)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //
        }

        // db bedzie potrzebny do stworzenia w bazie tabeli category
        public DbSet<Category> Categories { get; set; }
    }
}
