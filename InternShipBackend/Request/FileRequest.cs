using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternShipBackend.Request
{
    public class FileRequest
    {
        public int studentId { get; set; }
        public IFormFile file { get; set; }
    }
}
