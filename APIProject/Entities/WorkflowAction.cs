using System.ComponentModel.DataAnnotations.Schema;

namespace APIProject.Entities
{
    public class WorkflowAction
    {
        public int WorkflowActionId { get; set; }

        // Foreign key properties
        public int WorkflowId { get; set; }

        public string? Action { get; set; }

        public int StateFromWorkflowStateId { get; set; }
        [ForeignKey(nameof(StateFromWorkflowStateId))]
        public WorkflowState StateFromwWorkflowState { get; set; }


        public int StateToWorkflowStateId { get; set; }
        [ForeignKey(nameof(StateToWorkflowStateId))]
        public WorkflowState StateToWorkflowState { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public virtual Workflow Workflow { get; set; }
    }
}
