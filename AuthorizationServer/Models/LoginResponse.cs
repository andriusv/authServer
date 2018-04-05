using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string ReturnUrl { get; set; }
        public string Error { get; set; }
    }
}
