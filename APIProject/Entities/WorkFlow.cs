namespace APIProject.Entities
{
    public class WorkFlow
    {
        public int Id { get; set; }   
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        // Foreign key properties
        public int EmployeeId { get; set; }
        public int StateId { get; set; }

        // Navigation properties (optional)
        public Employee Employee { get; set; }
        public State State { get; set; }
    }
}
