using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Student
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public int companyId { get; set; }

        public long tcNo { get; set; }
        public string profilePicture { get; set; }
        public Company Company { get; set; }
        public List<Day> Day { get; set; }
      




    }
}
