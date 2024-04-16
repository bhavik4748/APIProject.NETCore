using APIProject.Data;
using APIProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public EmployeeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetAllEmployees()
        {
            var employees = await _dataContext.employees.ToListAsync();
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _dataContext.employees.FindAsync(id);
            if (employee is null)
                return NotFound("Employee not found.");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            employee.CreatedDate = DateTime.Now;
            employee.ModifiedDate = DateTime.Now;
            _dataContext.employees.Add(employee);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.employees.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            var dbEmployee = await _dataContext.employees.FindAsync(employee.Id);
            if (dbEmployee is null)
                return NotFound("Employee not found.");

            dbEmployee.Name = employee.Name;
            dbEmployee.ModifiedDate = DateTime.Now;
          
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.employees.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<Employee>> DeleteEmployee(Employee employee)
        {
            var dbEmployee = await _dataContext.employees.FindAsync(employee.Id);
            if (dbEmployee is null)
                return NotFound("Employee not found.");

            _dataContext.employees.Remove(dbEmployee);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.employees.ToListAsync());
        }
    }
}
