using APIProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace APIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<WorkflowState> WorkflowStates { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }

        public DbSet<EmployeeWorkflowState> EmployeeWorkflowStates { get; set; }

        public DbSet<EmployeeWorkflowAction> EmployeeWorkflowActions { get; set; }
        public DbSet<Audit> Audits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Entity A
            modelBuilder.Entity<WorkflowState>()
                .HasKey(a => a.WorkflowStateId);

            // Define Entity B
            modelBuilder.Entity<WorkflowAction>()
                .HasKey(b => b.WorkflowActionId);

            // Define the relationship
            modelBuilder.Entity<WorkflowAction>()
                .HasOne(b => b.StateFromwWorkflowState)
                .WithMany()
                .HasForeignKey(b => b.StateFromWorkflowStateId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            modelBuilder.Entity<WorkflowAction>()
                .HasOne(b => b.StateToWorkflowState)
                .WithMany()
                .HasForeignKey(b => b.StateToWorkflowStateId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete
        }

    }

}
