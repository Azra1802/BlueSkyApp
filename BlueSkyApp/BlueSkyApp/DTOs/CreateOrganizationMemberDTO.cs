namespace BlueSkyApp.DTOs
{
    public class CreateOrganizationMemberDTO
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
