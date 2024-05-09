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
    public class WorkflowActionsController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkflowActionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/WorkflowActions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkflowAction>>> GetWorkflowActions()
        {
            return await _context.WorkflowActions.ToListAsync();
        }

        // GET: api/WorkflowActions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkflowAction>> GetWorkflowAction(int id)
        {
            var workflowAction = await _context.WorkflowActions.FindAsync(id);

            if (workflowAction == null)
            {
                return NotFound();
            }

            return workflowAction;
        }

        // PUT: api/WorkflowActions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkflowAction(int id, WorkflowAction workflowAction)
        {
            if (id != workflowAction.WorkflowActionId)
            {
                return BadRequest();
            }

            _context.Entry(workflowAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowActionExists(id))
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

        // POST: api/WorkflowActions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkflowAction>> PostWorkflowAction(WorkflowAction workflowAction)
        {
            var workflowId = workflowAction.WorkflowId;
            var workflow = await _context.Workflows.FindAsync(workflowId);

            if (workflow == null)
            {
                return NotFound("Workflow not exist");
            }
            workflowAction.Workflow = null;
            _context.WorkflowActions.Add(workflowAction);
            await _context.SaveChangesAsync();

            var result = await _context.WorkflowActions
               .Include(w => w.Workflow)
               .ToListAsync();

            return Ok(result);           
        }

        // DELETE: api/WorkflowActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflowAction(int id)
        {
            var workflowAction = await _context.WorkflowActions.FindAsync(id);
            if (workflowAction == null)
            {
                return NotFound();
            }

            _context.WorkflowActions.Remove(workflowAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkflowActionExists(int id)
        {
            return _context.WorkflowActions.Any(e => e.WorkflowActionId == id);
        }
    }
}
