using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RolesController(ApplicationDbContext db) => _db = db;

       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _db.Roles
                .Select(r => new RoleDTO
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description
                }).ToListAsync();

            return Ok(roles);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var role = await _db.Roles
                .Where(r => r.RoleId == id)
                .Select(r => new RoleDTO
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description
                }).FirstOrDefaultAsync();

            if (role == null) return NotFound();
            return Ok(role);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post(CreateRoleDTO dto)
        {
            var role = new Data.Role
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            var resultDto = new RoleDTO
            {
                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description
            };

            return CreatedAtAction(nameof(Get), new { id = role.RoleId }, resultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RoleDTO dto)
        {
            var role = await _db.Roles.FindAsync(id);
            if (role == null) return NotFound();

            role.Name = dto.Name;
            role.Description = dto.Description;

            _db.Entry(role).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _db.Roles.FindAsync(id);
            if (role == null) return NotFound();

            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
