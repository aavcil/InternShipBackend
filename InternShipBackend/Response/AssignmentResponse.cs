﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Response
{
    public class AssignmentResponse
    {
        public int id { get; set; }

        public int studentId { get; set; }

        public int teacherId { get; set; }

        public Student Student { get; set; }

        public Teacher Teachers { get; set; }
    }
}
