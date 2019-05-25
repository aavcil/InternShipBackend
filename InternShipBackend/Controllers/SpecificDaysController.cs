using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SpecificDaysController : ControllerBase
    {
        private IAppRepository _appRepository;

        public SpecificDaysController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]
        public IActionResult GetSpecDay()
        {
            var date = _appRepository.GetDates();
            return Ok(date);
        }
        [HttpPost]
        public IActionResult SetSpecDay([FromBody] SpecificDayRequest req)
        {
            var date = new SpecificDays()
            {
                startDate = req.startDate,
                finishDate = req.finishDate
            };
            _appRepository.Add(date);
            _appRepository.SaveAll();
            return Ok();
        }
    }
}