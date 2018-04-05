using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class RegisterUserResponse
    {
        public string userName { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
