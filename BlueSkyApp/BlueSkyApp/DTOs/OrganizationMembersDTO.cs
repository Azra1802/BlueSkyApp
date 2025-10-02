namespace BlueSkyApp.DTOs
{
    public class OrganizationMemberDTO
    {
        public int OrganizationMemberId { get; set; }
        public int? OrganizationId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
