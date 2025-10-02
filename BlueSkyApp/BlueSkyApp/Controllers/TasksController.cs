using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = BlueSkyApp.Data.Task;

namespace BlueSkyApp.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TasksController(ApplicationDbContext db) => _db = db;

      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _db.Tasks
                .Select(t => new TaskDTO
                {
                    TaskId = t.TaskId,
                    ApartmentId = t.ApartmentId,
                    PropertyId = t.PropertyId,
                    AssignedTo = t.AssignedTo,
                    TaskType = t.TaskType,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt
                }).ToListAsync();

            return Ok(tasks);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _db.Tasks
                .Where(t => t.TaskId == id)
                .Select(t => new TaskDTO
                {
                    TaskId = t.TaskId,
                    ApartmentId = t.ApartmentId,
                    PropertyId = t.PropertyId,
                    AssignedTo = t.AssignedTo,
                    TaskType = t.TaskType,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt
                }).FirstOrDefaultAsync();

            if (task == null) return NotFound();
            return Ok(task);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post(CreateTaskDTO dto)
        {
            var task = new Task
            {
                ApartmentId = dto.ApartmentId,
                PropertyId = dto.PropertyId,
                AssignedTo = dto.AssignedTo,
                TaskType = dto.TaskType,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            var resultDto = new TaskDTO
            {
                TaskId = task.TaskId,
                ApartmentId = task.ApartmentId,
                PropertyId = task.PropertyId,
                AssignedTo = task.AssignedTo,
                TaskType = task.TaskType,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = task.TaskId }, resultDto);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TaskDTO dto)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.ApartmentId = dto.ApartmentId;
            task.PropertyId = dto.PropertyId;
            task.AssignedTo = dto.AssignedTo;
            task.TaskType = dto.TaskType;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

            _db.Entry(task).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
