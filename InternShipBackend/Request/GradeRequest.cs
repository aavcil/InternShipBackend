using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class GradeRequest
    {
        public int studentId { get; set; }
        public int vize { get; set; }
        public int final { get; set; }
    }
}
