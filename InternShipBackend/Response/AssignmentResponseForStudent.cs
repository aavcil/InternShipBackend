using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Response
{
    public class AssignmentResponseForStudent
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string title { get; set; }

    }
}
