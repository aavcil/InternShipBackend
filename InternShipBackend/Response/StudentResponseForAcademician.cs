using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Response
{
    public class StudentResponseForAcademician
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public long tcNo { get; set; }
        public string profilePicture { get; set; }



    }
}
