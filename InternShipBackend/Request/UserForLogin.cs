using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class UserForLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
