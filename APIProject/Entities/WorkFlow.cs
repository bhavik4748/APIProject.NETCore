namespace APIProject.Entities
{
    public class Workflow
    {
        public int WorkflowId { get; set; }


        public  string? WorkflowName { get; set; }
        public string WorkflowDescription { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    }
}
