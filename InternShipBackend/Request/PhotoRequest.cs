using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternShipBackend.Request
{
    public class PhotoRequest
    {
        public string url { get; set; }
        public int dayId { get; set; }
        public string randomWord { get; set; }
    }
}
