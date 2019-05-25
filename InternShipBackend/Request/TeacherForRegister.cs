using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class TeacherForRegister
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string name { get; set; }

        public string surname { get; set; }

        public string title { get; set; }

        public int userGroup { get; set; }
    }
}
