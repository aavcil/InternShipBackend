using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class File
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public string path { get; set; }

    }
}
