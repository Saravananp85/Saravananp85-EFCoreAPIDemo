using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EFCoreAPIDemo.Models
{
    public partial class User
    {
        public User()
        {
            UserSupervisorMappingSupervisors = new HashSet<UserSupervisorMapping>();
            UserSupervisorMappingUsers = new HashSet<UserSupervisorMapping>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserSupervisorMapping> UserSupervisorMappingSupervisors { get; set; }
        public virtual ICollection<UserSupervisorMapping> UserSupervisorMappingUsers { get; set; }
    }
}
