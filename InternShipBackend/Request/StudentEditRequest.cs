using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class StudentEditRequest
    {
        public int id { get; set; }
        public string profilePicture { get; set; }
        public string name { get; set; }

    }
}
