using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public InventoryController(ApplicationDbContext db) => _db = db;

     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var inventories = await _db.Inventories
                .Select(i => new InventoryDTO
                {
                    InventoryId = i.InventoryId,
                    ApartmentId = i.ApartmentId,
                    PropertyId = i.PropertyId,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    MinimumQuantity = i.MinimumQuantity,
                    CreatedAt = i.CreatedAt
                })
                .ToListAsync();

            return Ok(inventories);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var inventory = await _db.Inventories
                .Where(i => i.InventoryId == id)
                .Select(i => new InventoryDTO
                {
                    InventoryId = i.InventoryId,
                    ApartmentId = i.ApartmentId,
                    PropertyId = i.PropertyId,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    MinimumQuantity = i.MinimumQuantity,
                    CreatedAt = i.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post(CreateInventoryDTO dto)
        {
            var inventory = new Inventory
            {
                ApartmentId = dto.ApartmentId,
                PropertyId = dto.PropertyId,
                Name = dto.Name,
                Quantity = dto.Quantity,
                MinimumQuantity = dto.MinimumQuantity,
                CreatedAt = DateTime.UtcNow
            };

            _db.Inventories.Add(inventory);
            await _db.SaveChangesAsync();

            var resultDto = new InventoryDTO
            {
                InventoryId = inventory.InventoryId,
                ApartmentId = inventory.ApartmentId,
                PropertyId = inventory.PropertyId,
                Name = inventory.Name,
                Quantity = inventory.Quantity,
                MinimumQuantity = inventory.MinimumQuantity,
                CreatedAt = inventory.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = inventory.InventoryId }, resultDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InventoryDTO dto)
        {
            var inventory = await _db.Inventories.FindAsync(id);
            if (inventory == null) return NotFound();

            inventory.ApartmentId = dto.ApartmentId;
            inventory.PropertyId = dto.PropertyId;
            inventory.Name = dto.Name;
            inventory.Quantity = dto.Quantity;
            inventory.MinimumQuantity = dto.MinimumQuantity;

            _db.Entry(inventory).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _db.Inventories.FindAsync(id);
            if (inventory == null) return NotFound();

            _db.Inventories.Remove(inventory);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
