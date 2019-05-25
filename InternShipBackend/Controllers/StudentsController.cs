using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class StudentsController : ControllerBase
    {
        private IAppRepository _appRepository;

        public StudentsController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]
        public IActionResult getStudents()
        {
            var students = _appRepository.GetStudents();
            return Ok(students);
        }

        [HttpGet("getStudentById")]
        public IActionResult getStudent(int id)
        {
            var student = _appRepository.getStudent(id);
            return Ok(student);
        }
        [HttpGet("getStudentForHome")]
        public IActionResult getStudentForHome()
        {
            var student = _appRepository.GetStudentForHome();
            return Ok(student);
        }

        [HttpPost("editStudent")]
        public IActionResult editStudent([FromBody] StudentEditRequest student)
        {
            string profilePictureUrl = null;

            string result = Regex.Replace(student.profilePicture, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (student.profilePicture != null)
            {
                profilePictureUrl = $"images/{student.name}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath =
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }

            if (profilePictureUrl != null)
            {
                return Ok(_appRepository.editStudent(profilePictureUrl, student.id));
            }

            return StatusCode(400);
        }

       
        
    }
}