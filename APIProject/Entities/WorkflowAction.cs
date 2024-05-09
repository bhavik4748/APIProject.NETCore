namespace APIProject.Entities
{
    public class WorkflowAction
    {
        public int WorkflowActionId { get; set; }

        // Foreign key properties
        public int WorkflowId { get; set; }

        public string? Action { get; set; }
        public string? StateFrom { get; set; }
        public string? StateTo { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public virtual Workflow Workflow { get; set; }
    }
}
