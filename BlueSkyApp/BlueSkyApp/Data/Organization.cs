using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class Organization
    {
        public Organization()
        {
            OrganizationMembers = new HashSet<OrganizationMember>();
            Properties = new HashSet<Property>();
        }

        public int OrganizationId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
