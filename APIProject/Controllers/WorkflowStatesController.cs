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
using Microsoft.AspNetCore.Http.HttpResults;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStatesController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkflowStatesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/WorkflowStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkflowState>>> GetWorkflowStates()
        {
            return await _context.WorkflowStates.ToListAsync();
        }

        // GET: api/WorkflowStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkflowState>> GetWorkflowState(int id)
        {
            var workflowState = await _context.WorkflowStates.FindAsync(id);

            if (workflowState == null)
            {
                return NotFound();
            }

            return workflowState;
        }

        // PUT: api/WorkflowStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkflowState(int id, WorkflowState workflowState)
        {
            if (id != workflowState.WorkflowStateId)
            {
                return BadRequest();
            }

            _context.Entry(workflowState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowStateExists(id))
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

        // POST: api/WorkflowStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkflowState>> PostWorkflowState(WorkflowState workflowState)
        {
            var workflowId = workflowState.WorkflowId;
            var workflow = await _context.Workflows.FindAsync(workflowId);

            if (workflow == null)
            {
                return  NotFound("Workflow not exist");
            }
            workflowState.Workflow = null;
            _context.WorkflowStates.Add(workflowState);
            await _context.SaveChangesAsync();

             var result = await _context.WorkflowStates               
                .Include(w => w.Workflow)
                .ToListAsync();

            return Ok(result);
        }

        // DELETE: api/WorkflowStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflowState(int id)
        {
            var workflowState = await _context.WorkflowStates.FindAsync(id);
            if (workflowState == null)
            {
                return NotFound();
            }

            _context.WorkflowStates.Remove(workflowState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkflowStateExists(int id)
        {
            return _context.WorkflowStates.Any(e => e.WorkflowStateId == id);
        }
    }
}
