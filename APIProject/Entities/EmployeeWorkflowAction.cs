namespace APIProject.Entities
{
    public class EmployeeWorkflowAction
    {
        public int EmployeeWorkflowActionId { get; set; }

        public int EmployeeId { get; set; }
        public int WorkflowActionId { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual WorkflowAction? WorkflowAction { get; set; }

    }
}
