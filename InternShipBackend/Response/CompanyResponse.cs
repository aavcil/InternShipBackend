using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Response
{
    public class CompanyResponse
    {
        public int id{ get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string telephone { get; set; }

        public string mail { get; set; }

        public string logoUrl { get; set; }

        public int personelCount { get; set; }
    }
}
