namespace BlueSkyApp.DTOs
{
        public class CreateTaskDTO
        {
            public int? ApartmentId { get; set; }
            public int? PropertyId { get; set; }
            public int? AssignedTo { get; set; }
            public string? TaskType { get; set; }
            public string? Description { get; set; }
            public string? Status { get; set; }
            public DateTime? DueDate { get; set; }
        }
    }
