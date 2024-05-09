﻿using APIProject.Data;
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
            employee.CreatedDate = DateTime.UtcNow;
            employee.ModifiedDate = DateTime.UtcNow;
            _dataContext.Employees.Add(employee);
            await _dataContext.SaveChangesAsync();
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
