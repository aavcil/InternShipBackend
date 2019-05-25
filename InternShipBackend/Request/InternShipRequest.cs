using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class InternShipRequest
    {

        public DateTime startDate { get; set; }

        public DateTime finishDate { get; set; }

        public int studentId { get; set; }
    }
}
