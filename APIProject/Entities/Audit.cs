namespace APIProject.Entities
{
    public class Audit
    {
        public int AuditId { get; set; }   

        public string? DataTableName { get; set; }

        public int DataTableId { get; set; }

        // Foreign key properties
        public int WorkFlowId { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        public virtual Workflow Workflow { get; set; }
        
    }
}
