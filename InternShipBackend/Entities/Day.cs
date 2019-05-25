using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Day
    {
        public int id { get; set; }

        public int studentId { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }
        public DateTime date { get; set; }
        public List<Photo>Photos { get; set; }

    }
}
