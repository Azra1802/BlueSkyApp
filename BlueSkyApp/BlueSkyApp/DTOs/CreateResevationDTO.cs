namespace BlueSkyApp.Controllers
{
    public class CreateReservationDTO
    {
        public int? ApartmentId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
    }
}
