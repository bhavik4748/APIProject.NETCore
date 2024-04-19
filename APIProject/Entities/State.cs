using APIProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace APIProject.Entities
{
    public class State
    {
        public int StateId { get; set; }
        public required string StateName { get; set; }
        public string StateDescription { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }


}
