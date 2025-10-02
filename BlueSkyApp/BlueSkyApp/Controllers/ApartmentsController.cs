using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ApartmentsController(ApplicationDbContext db) => _db = db;

       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var apartments = await _db.Apartments
                .Select(a => new ApartmentDTO
                {
                    ApartmentId = a.ApartmentId,
                    PropertyId = a.PropertyId,
                    Name = a.Name,
                    Floor = a.Floor,
                    NumberOfRooms = a.NumberOfRooms,
                    PricePerNight = a.PricePerNight,
                    Capacity = a.Capacity,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var apartment = await _db.Apartments
                .Where(a => a.ApartmentId == id)
                .Select(a => new ApartmentDTO
                {
                    ApartmentId = a.ApartmentId,
                    PropertyId = a.PropertyId,
                    Name = a.Name,
                    Floor = a.Floor,
                    NumberOfRooms = a.NumberOfRooms,
                    PricePerNight = a.PricePerNight,
                    Capacity = a.Capacity,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (apartment == null) return NotFound();
            return Ok(apartment);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post(CreateApartmentDTO apartmentDto)
        {
            var apartment = new Apartment
            {
                PropertyId = apartmentDto.PropertyId,
                Name = apartmentDto.Name,
                Floor = apartmentDto.Floor,
                NumberOfRooms = apartmentDto.NumberOfRooms,
                PricePerNight = apartmentDto.PricePerNight,
                Capacity = apartmentDto.Capacity,
                CreatedAt = DateTime.UtcNow
            };

            _db.Apartments.Add(apartment);
            await _db.SaveChangesAsync();

            var resultDto = new ApartmentDTO
            {
                ApartmentId = apartment.ApartmentId,
                PropertyId = apartment.PropertyId,
                Name = apartment.Name,
                Floor = apartment.Floor,
                NumberOfRooms = apartment.NumberOfRooms,
                PricePerNight = apartment.PricePerNight,
                Capacity = apartment.Capacity,
                CreatedAt = apartment.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = apartment.ApartmentId }, resultDto);
        }

        // PUT: /Apartments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ApartmentDTO apartmentDto)
        {
            var apartment = await _db.Apartments.FindAsync(id);
            if (apartment == null) return NotFound();

            apartment.Name = apartmentDto.Name;
            apartment.Floor = apartmentDto.Floor;
            apartment.NumberOfRooms = apartmentDto.NumberOfRooms;
            apartment.PricePerNight = apartmentDto.PricePerNight;
            apartment.Capacity = apartmentDto.Capacity;
            apartment.PropertyId = apartmentDto.PropertyId;

            _db.Entry(apartment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /Apartments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var apartment = await _db.Apartments.FindAsync(id);
            if (apartment == null) return NotFound();

            _db.Apartments.Remove(apartment);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
