using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Assignment
    {
        public int id { get; set; }

        public int studentId { get; set; }

        public int teacherId { get; set; }

        public  Student Student { get; set; }

        public Teacher Teachers { get; set; }
    }
}
