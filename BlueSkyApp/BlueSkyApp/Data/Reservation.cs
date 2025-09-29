using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int? ApartmentId { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Apartment? Apartment { get; set; }
    }
}
