using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreAPIDemo.Models
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage ="emailId is required")]
        public string emailId { get; set; }
    }
}
