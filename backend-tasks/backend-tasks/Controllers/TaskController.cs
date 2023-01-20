using backend_tasks.Context;
using backend_tasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace backend_tasks.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly AplicationDbContext _context;
        public TaskController(AplicationDbContext context)
        {
            _context= context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listTareas = await _context.Task.ToListAsync();
                return Ok(listTareas);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tasks task)
        {
            try
            {
                _context.Task.Add(task);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Task was successfull" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Tasks task)
        {
            try
            {
                if(id != task.Id)
                {
                    return NotFound();
                }
                else
                {
                    task.Estado = !task.Estado;
                    _context.Entry(task).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new {message = "Task was successfull" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarea = await _context.Task.FindAsync(id);
                if (tarea == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Task.Remove(tarea);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Task was successfull"});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
