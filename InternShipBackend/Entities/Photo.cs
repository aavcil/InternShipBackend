﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Entities
{
    public class Photo
    {
        public int id { get; set; }
        public string url { get; set; }
        public int dayId { get; set; } 
    }
}
