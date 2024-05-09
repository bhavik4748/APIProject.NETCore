using APIProject.Data;
using APIProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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
            var employees = await _dataContext.Employees.ToListAsync();
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee is null)
                return NotFound("Employee not found.");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var initialStateId = 1; // hardcode
            var employeeNew = new Employee
            {
                Name = employee.Name,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };
            var employeeWorkflowDefaultState = new EmployeeWorkflowState
            {
                Employee = employeeNew,
                WorkflowStateId = initialStateId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            _dataContext.EmployeeWorkflowStates.Add(employeeWorkflowDefaultState);
            await _dataContext.SaveChangesAsync();

            //    _dataContext.Employees.Add(employeeNew);

            //   // var employeeId = employeeNew.EmployeeId;



            //  //  employeeWorkflowDefaultState.EmployeeId = employeeId;
            //    //employeeWorkflowDefaultState.EmployeeWorkflowStateId = initialStateId;
            //    _dataContext.EmployeeWorkflowStates.Add(employeeWorkflowDefaultState);
            //    await _dataContext.SaveChangesAsync();


            //var employeeNew = new Employee { Name = employee.Name, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow }; //no Id yet;
            //_dataContext.Employees.Add(employeeNew);
            //await _dataContext.SaveChangesAsync();
            //await _dataContext.Entry(employeeNew).GetDatabaseValuesAsync();


            //var employeeWorkflowState = new EmployeeWorkflowState { EmployeeId = employeeNew.EmployeeId, WorkflowStateId = initialStateId };
            //_dataContext.EmployeeWorkflowStates.Add(employeeWorkflowState);
            //await _dataContext.SaveChangesAsync(); //adds customer.Id to customer and the correct CustomerId to order

            //   await _dataContext.Entry(employeeWorkflowState).GetDatabaseValuesAsync();

            return Ok(await _dataContext.Employees.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            var dbEmployee = await _dataContext.Employees.FindAsync(employee.EmployeeId);
            if (dbEmployee is null)
                return NotFound("Employee not found.");

            dbEmployee.Name = employee.Name;
            dbEmployee.ModifiedDate = DateTime.UtcNow;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Employees.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<Employee>> DeleteEmployee(Employee employee)
        {
            var dbEmployee = await _dataContext.Employees.FindAsync(employee.EmployeeId);
            if (dbEmployee is null)
                return NotFound("Employee not found.");

            _dataContext.Employees.Remove(dbEmployee);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Employees.ToListAsync());
        }
    }
}
