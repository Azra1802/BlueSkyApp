using System;
using System.Collections.Generic;

namespace BlueSkyApp.Models
{
    public class ApartmentModel
    {
        public int ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public DateTime? CreatedAt { get; set; }

       
        public PropertyModel? Property { get; set; }
        public List<InventoryModel>? Inventories { get; set; }
        public List<ReservationModel>? Reservations { get; set; }
        public List<TaskModel>? Tasks { get; set; }

        public ApartmentModel()
        {
            Inventories = new List<InventoryModel>();
            Reservations = new List<ReservationModel>();
            Tasks = new List<TaskModel>();
        }
    }
}
