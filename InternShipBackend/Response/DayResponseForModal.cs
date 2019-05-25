using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Response
{
    public class DayResponseForModal
    {
        public string description { get; set; }
        public string title { get; set; }
        public List<Photo> Photo { get; set; }
    }
}
