using System;

namespace BlueSkyApp.Models
{
    public class InventoryModel
    {
        public int InventoryId { get; set; }
        public int? ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public int? MinimumQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
