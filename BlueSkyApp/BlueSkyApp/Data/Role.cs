using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Role
    {
        public Role()
        {
            OrganizationMembers = new HashSet<OrganizationMember>();
        }

        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
    }
}
