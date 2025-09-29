using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Property
    {
        public Property()
        {
            Apartments = new HashSet<Apartment>();
            Inventories = new HashSet<Inventory>();
            Tasks = new HashSet<Task>();
        }

        public int PropertyId { get; set; }
        public int? OrganizationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
