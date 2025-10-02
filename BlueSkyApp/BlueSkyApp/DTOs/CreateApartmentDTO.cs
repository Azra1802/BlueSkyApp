namespace BlueSkyApp.DTOs
{
    public class CreateApartmentDTO
    {
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int NumberOfRooms { get; set; }
        public int Capacity { get; set; }     
        public decimal PricePerNight { get; set; }
        public int? Floor { get; set; }
    }
}
