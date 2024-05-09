namespace APIProject.Entities
{
    public class WorkflowState
    {
        public int WorkflowStateId { get; set; }

        // Foreign key properties
        public int WorkflowId { get; set; }
        public string? StateName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public virtual Workflow Workflow { get; set; }
    }
}
