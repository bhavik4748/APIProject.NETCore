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
    public class EmployeeWorkflowActionsController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeWorkflowActionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeWorkflowActions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWorkflowAction>>> GetEmployeeWorkflowActions()
        {
            return await _context.EmployeeWorkflowActions
                    .Include(w => w.Employee)
                    .Include(w => w.WorkflowAction)
                    .ToListAsync();
        }

        // GET: api/EmployeeWorkflowActions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWorkflowAction>> GetEmployeeWorkflowAction(int id)
        {
            var employeeWorkflowAction = await _context.EmployeeWorkflowActions.FindAsync(id);

            if (employeeWorkflowAction == null)
            {
                return NotFound();
            }

            var dbEmployee = await _context.Employees.FindAsync(employeeWorkflowAction.EmployeeId);
            var dbWorkflowAction = await _context.WorkflowActions.FindAsync(employeeWorkflowAction.WorkflowActionId);

            employeeWorkflowAction.Employee = dbEmployee;
            employeeWorkflowAction.WorkflowAction = dbWorkflowAction;

            return employeeWorkflowAction;
        }

        // PUT: api/EmployeeWorkflowActions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeWorkflowAction(int id, EmployeeWorkflowAction employeeWorkflowAction)
        {
            if (id != employeeWorkflowAction.EmployeeWorkflowActionId)
            {
                return BadRequest();
            }

            _context.Entry(employeeWorkflowAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeWorkflowActionExists(id))
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

        // POST: api/EmployeeWorkflowActions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeWorkflowAction>> PostEmployeeWorkflowAction(EmployeeWorkflowAction employeeWorkflowAction)
        {
            var employeeId = employeeWorkflowAction.EmployeeId;
            var workflowActionId = employeeWorkflowAction.WorkflowActionId;

            var dbEmployee = await _context.Employees.FindAsync(employeeId);
            var dbWorkflowAction = await _context.WorkflowStates.FindAsync(workflowActionId);

            if (dbEmployee == null || dbEmployee == null)
            {
                return NotFound("Employee or Workflow Action does not exist");
            }

            employeeWorkflowAction.WorkflowAction = null;
            employeeWorkflowAction.Employee = null;

            _context.EmployeeWorkflowActions.Add(employeeWorkflowAction);
            await _context.SaveChangesAsync();

            var result = await _context.EmployeeWorkflowActions
             .Include(w => w.Employee)
             .Include(w => w.WorkflowAction)
             .ToListAsync();

            return Ok(result);
        }

        // DELETE: api/EmployeeWorkflowActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeWorkflowAction(int id)
        {
            var employeeWorkflowAction = await _context.EmployeeWorkflowActions.FindAsync(id);
            if (employeeWorkflowAction == null)
            {
                return NotFound();
            }

            _context.EmployeeWorkflowActions.Remove(employeeWorkflowAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeWorkflowActionExists(int id)
        {
            return _context.EmployeeWorkflowActions.Any(e => e.EmployeeWorkflowActionId == id);
        }
    }
}
