using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Recourse
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public string url { get; set; }
        public int isApproved { get; set; }
        public DateTime date { get; set; }
        public Student Student { get; set; }

    }
}
