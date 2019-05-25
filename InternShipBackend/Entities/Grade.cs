using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Grade
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public int vize { get; set; }
        public int final { get; set; }
    }
}
