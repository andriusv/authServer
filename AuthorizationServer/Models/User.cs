using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class ApplicationUser : IdentityUser
    {          
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordOld1 { get; set; }
        public string PasswordOld2 { get; set; }
        public string PasswordOld3 { get; set; }
        public string PasswordOld4 { get; set; }
    }
}
