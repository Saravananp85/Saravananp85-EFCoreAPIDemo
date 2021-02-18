using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreAPIDemo.Models
{
    public partial class UserSupervisorMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SupervisorId { get; set; }

        public virtual User Supervisor { get; set; }
        public virtual User User { get; set; }
    }
}
