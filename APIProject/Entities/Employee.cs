namespace APIProject.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
