using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Data;

namespace TaskFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(TaskDbContext context) : ControllerBase
    {

        private readonly TaskDbContext _context;


        [HttpGet]
        public async Task<ActionResult<List<Chore>>> GetTasks()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Chore>> GetTaskById(long id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<Chore>> AddTask(Chore task)
        {
            if(task is null)
                return BadRequest();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("`{id}")]
        public async Task<IActionResult> UpadteTask(int id, Chore updatedTask)
        {
            if (id != updatedTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the item.");
                }
            }

            return Ok(updatedTask);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }   
    }
}
