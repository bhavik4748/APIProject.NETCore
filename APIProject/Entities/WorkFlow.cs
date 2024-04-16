namespace APIProject.Entities
{
    public class WorkFlow
    {
        public int Id { get; set; }
        public int StateId { get; set; } // Foreign key to State

        public int EmployeeId { get; set; } // Foreign key to Employee

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }

        // Navigation properties
        public virtual required Employee Employee { get; set; }
        public virtual required State State { get; set; }
    }
}
