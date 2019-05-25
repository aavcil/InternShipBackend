using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Response
{
    public class RecourseResponse
    {
        public int id { get; set; }
        public string url { get; set; }
        public int isApproved { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public int studentId { get; set; }

        public string surname { get; set; }
    }
}
