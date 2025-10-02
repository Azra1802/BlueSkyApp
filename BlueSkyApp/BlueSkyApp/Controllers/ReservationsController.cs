using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ReservationsController(ApplicationDbContext db) => _db = db;

    
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservations = await _db.Reservations
                .Select(r => new ReservationDTO
                {
                    ReservationId = r.ReservationId,
                    ApartmentId = r.ApartmentId,
                    GuestName = r.GuestName,
                    GuestEmail = r.GuestEmail,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();

            return Ok(reservations);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reservation = await _db.Reservations
                .Where(r => r.ReservationId == id)
                .Select(r => new ReservationDTO
                {
                    ReservationId = r.ReservationId,
                    ApartmentId = r.ApartmentId,
                    GuestName = r.GuestName,
                    GuestEmail = r.GuestEmail,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                }).FirstOrDefaultAsync();

            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(CreateReservationDTO dto)
        {
            var reservation = new Data.Reservation
            {
                ApartmentId = dto.ApartmentId,
                GuestName = dto.GuestName,
                GuestEmail = dto.GuestEmail,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();

            var resultDto = new ReservationDTO
            {
                ReservationId = reservation.ReservationId,
                ApartmentId = reservation.ApartmentId,
                GuestName = reservation.GuestName,
                GuestEmail = reservation.GuestEmail,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                CreatedAt = reservation.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = reservation.ReservationId }, resultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ReservationDTO dto)
        {
            var reservation = await _db.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.ApartmentId = dto.ApartmentId;
            reservation.GuestName = dto.GuestName;
            reservation.GuestEmail = dto.GuestEmail;
            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;
            reservation.Status = dto.Status;

            _db.Entry(reservation).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _db.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            _db.Reservations.Remove(reservation);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
