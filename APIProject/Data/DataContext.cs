using APIProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace APIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }

        public DbSet<State> states { get; set; }

        public DbSet<WorkFlow> workflows { get; set; }

    }
}
