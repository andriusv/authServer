using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Data
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
