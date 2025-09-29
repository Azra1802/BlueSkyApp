using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Apartment
    {
        public Apartment()
        {
            Inventories = new HashSet<Inventory>();
            Reservations = new HashSet<Reservation>();
            Tasks = new HashSet<Task>();
        }

        public int ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Property? Property { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
