namespace BlueSkyApp.DTOs
{
    public class CreateInventoryDTO
    {
        public int? ApartmentId { get; set; }
        public int? PropertyId { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public int? MinimumQuantity { get; set; }
    }
}
