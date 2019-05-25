using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Response
{
    public class TeacherLoginResponse
    {
        public int Id { get; set; }
        public string tokenString { get; set; }
        public string Username { get; set; }
        public TeacherResponse Teacher { get; set; }
    }
}
