using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProject.Data;
using APIProject.Entities;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeWorkflowStatesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeWorkflowStatesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeWorkflowStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWorkflowState>>> GetEmployeeWorkflowStates()
        {
            return await _context.EmployeeWorkflowStates
                    .Include(w => w.Employee)
                    .Include(w => w.WorkflowState)
                    .ToListAsync();
        }

        // GET: api/EmployeeWorkflowStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWorkflowState>> GetEmployeeWorkflowState(int id)
        {
            var employeeWorkflowState = await _context.EmployeeWorkflowStates.FindAsync(id);

            if (employeeWorkflowState == null)
            {
                return NotFound();
            }

            var dbEmployee = await _context.Employees.FindAsync(employeeWorkflowState.EmployeeId);
            var dbWorkflowState = await _context.WorkflowStates.FindAsync(employeeWorkflowState.WorkflowStateId);

            employeeWorkflowState.Employee = dbEmployee;
            employeeWorkflowState.WorkflowState = dbWorkflowState;

            return employeeWorkflowState;
        }

        // PUT: api/EmployeeWorkflowStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeWorkflowState(int id, EmployeeWorkflowState employeeWorkflowState)
        {
            if (id != employeeWorkflowState.EmployeeWorkflowStateId)
            {
                return BadRequest();
            }

            _context.Entry(employeeWorkflowState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeWorkflowStateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeWorkflowStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeWorkflowState>> PostEmployeeWorkflowState(EmployeeWorkflowState employeeWorkflowState)
        {
            var employeeId = employeeWorkflowState.EmployeeId;
            var workflowStateId = employeeWorkflowState.WorkflowStateId;

            var dbEmployee = await _context.Employees.FindAsync(employeeId);
            var dbWorkflowState = await _context.WorkflowStates.FindAsync(workflowStateId);

            if (dbEmployee == null || dbEmployee == null)
            {
                return NotFound("Employee or Workflow State does not exist");
            }

            employeeWorkflowState.WorkflowState = null;
            employeeWorkflowState.Employee = null;

            _context.EmployeeWorkflowStates.Add(employeeWorkflowState);
            await _context.SaveChangesAsync();


            var result = await _context.EmployeeWorkflowStates
              .Include(w => w.Employee)
              .Include(w => w.WorkflowState)
              .ToListAsync();

            return Ok(result);

        }

        // DELETE: api/EmployeeWorkflowStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeWorkflowState(int id)
        {
            var employeeWorkflowState = await _context.EmployeeWorkflowStates.FindAsync(id);
            if (employeeWorkflowState == null)
            {
                return NotFound();
            }

            _context.EmployeeWorkflowStates.Remove(employeeWorkflowState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeWorkflowStateExists(int id)
        {
            return _context.EmployeeWorkflowStates.Any(e => e.EmployeeWorkflowStateId == id);
        }
    }
}
