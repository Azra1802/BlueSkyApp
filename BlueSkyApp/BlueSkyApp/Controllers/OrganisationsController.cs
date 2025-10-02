using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public OrganizationsController(ApplicationDbContext db) => _db = db;

      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizations = await _db.Organizations
                .Select(o => new OrganizationDTO
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();

            return Ok(organizations);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var organization = await _db.Organizations
                .Where(o => o.OrganizationId == id)
                .Select(o => new OrganizationDTO
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    CreatedAt = o.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (organization == null) return NotFound();
            return Ok(organization);
        }

      
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrganizationDTO dto)
        {
            var organization = new Organization
            {
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _db.Organizations.Add(organization);
            await _db.SaveChangesAsync();

            var resultDto = new OrganizationDTO
            {
                OrganizationId = organization.OrganizationId,
                Name = organization.Name,
                CreatedAt = organization.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = organization.OrganizationId }, resultDto);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrganizationDTO dto)
        {
            var organization = await _db.Organizations.FindAsync(id);
            if (organization == null) return NotFound();

            organization.Name = dto.Name;

            _db.Entry(organization).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var organization = await _db.Organizations.FindAsync(id);
            if (organization == null) return NotFound();

            _db.Organizations.Remove(organization);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
