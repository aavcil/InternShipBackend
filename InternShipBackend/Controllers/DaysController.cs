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
    public class DaysController : ControllerBase
    {
        private IAppRepository _appRepository;

        public DaysController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]

        public IActionResult getDaysByStudent(int id)
        {
            var days = _appRepository.getDaysByStudent(id);
            return Ok(days);
        }

        [HttpPost]
        public IActionResult editDay([FromBody] DayRequest day)
        {

            return Ok(_appRepository.editDay(day));
        }

        [HttpGet("getDayInfo")]
        public IActionResult getDay(int id)
        {
            var x = _appRepository.GetDay(id);
            return Ok(x);
        }

        [HttpPost("addPhoto")]
        public IActionResult AddPhoto([FromBody] PhotoRequest photo)
        {
            string profilePictureUrl = null;

            string result = Regex.Replace(photo.url, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (photo.url != null)
            {
                profilePictureUrl = $"images/{photo.randomWord}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }

            var x = new Photo()
            {
                dayId = photo.dayId,
                url = profilePictureUrl

            };

            _appRepository.Add(x);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");

        }
    }
}