using System;

namespace BlueSkyApp.Models
{
    public class OrganizationMemberModel
    {
        public int OrganizationMemberId { get; set; }
        public int? OrganizationId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
