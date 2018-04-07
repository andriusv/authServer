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
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
    }
}
