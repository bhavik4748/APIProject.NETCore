using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProject.Data;
using APIProject.Entities;
using Mono.TextTemplating;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowsController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkFlowsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/WorkFlows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkFlow>>> Getworkflows()
        {
            return await _context.workflows
                .Include(w => w.Employee)
                .Include(w => w.State)
                .ToListAsync();
        }

        // GET: api/WorkFlows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkFlow>> GetWorkFlow(int id)
        {
            var workFlow = await _context.workflows.FindAsync(id);

            if (workFlow == null)
            {
                return NotFound();
            }

            var dbEmployee = await _context.employees.FindAsync(workFlow.EmployeeId);
            var dbState = await _context.states.FindAsync(workFlow.StateId);

            workFlow.Employee = dbEmployee;
            workFlow.State = dbState;
            return workFlow;
        }

        // PUT: api/WorkFlows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkFlow(int id, WorkFlow workFlow)
        {
            if (id != workFlow.Id)
            {
                return BadRequest();
            }

            _context.Entry(workFlow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFlowExists(id))
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

        // POST: api/WorkFlows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkFlow>> PostWorkFlow(WorkFlow workFlow)
        {
            var employeeId = workFlow.EmployeeId;
            var stateId = workFlow.StateId;

            var dbEmployee = await _context.employees.FindAsync(employeeId);
            var dbState = await _context.states.FindAsync(stateId);

            if (dbEmployee == null || dbState == null)
            {
                NotFound("Employee or State not exist");
            }

            workFlow.Employee = null;
            workFlow.State = null;

            _context.workflows.Add(workFlow);
            await _context.SaveChangesAsync();

            return Ok(await _context.workflows.ToListAsync()); ;
        }

        // DELETE: api/WorkFlows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkFlow(int id)
        {
            var workFlow = await _context.workflows.FindAsync(id);
            if (workFlow == null)
            {
                return NotFound();
            }

            _context.workflows.Remove(workFlow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkFlowExists(int id)
        {
            return _context.workflows.Any(e => e.Id == id);
        }
    }
}
