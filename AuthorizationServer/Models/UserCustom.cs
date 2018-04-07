using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class UserCustom
    {
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
