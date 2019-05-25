using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InternShipBackend.Data;
using InternShipBackend.Entities;
using InternShipBackend.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecourseController : ControllerBase
    {
        private IAppRepository _appRepository;

        public RecourseController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost]
        public IActionResult AddRecourse([FromBody] RecourseRequest req)
        {
            string profilePictureUrl = null;

            string result = Regex.Replace(req.url, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (req.url != null)
            {
                profilePictureUrl = $"images/{req.studentId.ToString()}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }

            if (profilePictureUrl != null)
            {
                var addRecourse = new Recourse()
                {
                    studentId = req.studentId,
                    isApproved = 0,
                    url = profilePictureUrl,
                    date = DateTime.Now
                };
                _appRepository.Add(addRecourse);
                return Ok(_appRepository.SaveAll());
            }

            return StatusCode(400);
        }

        [HttpGet]

        public IActionResult GetRecourses()
        {
            return Ok(_appRepository.GetRecourses());
        }

        [HttpGet("GetRecourseById")]
        public IActionResult GetRecourse(int id)
        {
            return Ok(_appRepository.getRecourse(id));
        }
        [HttpGet("GetRecoursesFalse")]
        public IActionResult GetRecourseFalse()
        {
            return Ok(_appRepository.GetRecoursesFalse());
        }
        [HttpGet("SetRecourse")]
        public IActionResult SetRecourse(int id)
        {
            return Ok(_appRepository.setRecourse(id));
        }
        [HttpGet("SetRecourseReject")]
        public IActionResult SetRecourseReject(int id)
        {
            return Ok(_appRepository.setRecourseReject(id));
        }

    }
}