using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Response
{
    public class InternShipResponse
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }

        public DateTime finishDate { get; set; }

        public int studentId { get; set; }
    }
}
