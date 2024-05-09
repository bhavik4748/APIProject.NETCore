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
    public class WorkflowsController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkflowsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Workflows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workflow>>> GetWorkflows()
        {
            return await _context.Workflows.ToListAsync();
        }

        // GET: api/Workflows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workflow>> GetWorkflow(int id)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
            {
                return NotFound();
            }

            return workflow;
        }

        // PUT: api/Workflows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkflow(int id, Workflow workflow)
        {
            if (id != workflow.WorkflowId)
            {
                return BadRequest();
            }

            _context.Entry(workflow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowExists(id))
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

        // POST: api/Workflows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workflow>> PostWorkflow(Workflow workflow)
        {
            _context.Workflows.Add(workflow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkflow", new { id = workflow.WorkflowId }, workflow);
        }

        // DELETE: api/Workflows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflow(int id)
        {
            var workflow = await _context.Workflows.FindAsync(id);
            if (workflow == null)
            {
                return NotFound();
            }

            _context.Workflows.Remove(workflow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkflowExists(int id)
        {
            return _context.Workflows.Any(e => e.WorkflowId == id);
        }
    }
}
