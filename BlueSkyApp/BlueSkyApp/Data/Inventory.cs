using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int? ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public int? MinimumQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual Property? Property { get; set; }
    }
}
