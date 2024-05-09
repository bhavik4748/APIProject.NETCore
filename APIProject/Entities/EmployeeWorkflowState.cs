namespace APIProject.Entities
{
    public class EmployeeWorkflowState
    {
        public int EmployeeWorkflowStateId { get; set; }

        public int EmployeeId { get; set; }
        public int WorkflowStateId { get; set; }       

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual WorkflowState WorkflowState { get; set; }

    }
}
