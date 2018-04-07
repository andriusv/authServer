using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class UserResponse
    {
        public bool Succeeded { get; set; }
        public IdentityResult Result { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
