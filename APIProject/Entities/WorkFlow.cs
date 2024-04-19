namespace APIProject.Entities
{
    public class WorkFlow
    {
        public int WorkFlowId { get; set; }   
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        // Foreign key properties
        public int EmployeeId { get; set; }
        public int StateId { get; set; }

        // Navigation properties (optional)
        public virtual Employee Employee { get; set; }
        public virtual State State { get; set; }
    }
}
