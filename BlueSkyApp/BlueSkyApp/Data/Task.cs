using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public int? ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public int? AssignedTo { get; set; }
        public string? TaskType { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual User? AssignedToNavigation { get; set; }
        public virtual Property? Property { get; set; }
    }
}
