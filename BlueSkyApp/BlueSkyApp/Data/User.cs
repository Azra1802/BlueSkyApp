using System;
using System.Collections.Generic;

namespace BlueSkyApp.Data
{
    public partial class User
    {
        public User()
        {
            OrganizationMembers = new HashSet<OrganizationMember>();
            Tasks = new HashSet<Task>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
