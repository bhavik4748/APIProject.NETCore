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

    }

}
