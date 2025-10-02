using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PropertiesController(ApplicationDbContext db) => _db = db;

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _db.Properties
                .Select(p => new PropertyDTO
                {
                    PropertyId = p.PropertyId,
                    OrganizationId = p.OrganizationId,
                    Name = p.Name,
                    Address = p.Address,
                    CreatedAt = p.CreatedAt
                }).ToListAsync();

            return Ok(properties);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var property = await _db.Properties
                .Where(p => p.PropertyId == id)
                .Select(p => new PropertyDTO
                {
                    PropertyId = p.PropertyId,
                    OrganizationId = p.OrganizationId,
                    Name = p.Name,
                    Address = p.Address,
                    CreatedAt = p.CreatedAt
                }).FirstOrDefaultAsync();

            if (property == null) return NotFound();
            return Ok(property);
        }

      
        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyDTO dto)
        {
            var property = new Property
            {
                OrganizationId = dto.OrganizationId,
                Name = dto.Name,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            _db.Properties.Add(property);
            await _db.SaveChangesAsync();

            var resultDto = new PropertyDTO
            {
                PropertyId = property.PropertyId,
                OrganizationId = property.OrganizationId,
                Name = property.Name,
                Address = property.Address,
                CreatedAt = property.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = property.PropertyId }, resultDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PropertyDTO dto)
        {
            var property = await _db.Properties.FindAsync(id);
            if (property == null) return NotFound();

            property.Name = dto.Name;
            property.Address = dto.Address;
            property.OrganizationId = dto.OrganizationId;

            _db.Entry(property).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _db.Properties.FindAsync(id);
            if (property == null) return NotFound();

            _db.Properties.Remove(property);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
