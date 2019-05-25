using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class AssignmentRequest
    {
        public int studentId { get; set; }

        public int teacherId { get; set; }
    }
}
