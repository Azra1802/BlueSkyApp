namespace BlueSkyApp.DTOs
{
    public class PropertyDTO
    {
        public int PropertyId { get; set; }
        public int? OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
