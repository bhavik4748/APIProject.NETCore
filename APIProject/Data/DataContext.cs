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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure foreign key relationships
            modelBuilder.Entity<WorkFlow>()
                .HasOne(w => w.Employee)
                .WithMany() // No navigation property on Employee side
                .HasForeignKey(w => w.EmployeeId);

            modelBuilder.Entity<WorkFlow>()
                .HasOne(w => w.State)
                .WithMany() // No navigation property on State side
                .HasForeignKey(w => w.StateId);
        }

    }
}
