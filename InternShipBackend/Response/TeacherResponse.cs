using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Response
{
    public class TeacherResponse
    {
        public int id { get; set; }
        public string name { get; set; }

        public string surname { get; set; }

        public string title { get; set; }

        public int userGroup { get; set; }
    }
}
