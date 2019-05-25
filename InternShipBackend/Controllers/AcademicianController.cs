using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicianController : ControllerBase
    {
        private IAppRepository _appRepository;

        public AcademicianController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var x = _appRepository.GetStudentsForAcademicians(id);
            return Ok(x);
        }
        [HttpGet("settings")]
        public IActionResult SetGrading(int num)
        {
            var x = _appRepository.GradingSettings(num);
            return Ok(x);
        }
        [HttpGet("getSettings")]
        public IActionResult GetGrading()
        {
            var x = _appRepository.GetSetting();
            return Ok(x);
        }
        [HttpGet("isMyStudent")]
        public IActionResult IsMyStudent(int studentId,int academicianId)
        {
            var x = _appRepository.IsMyStudent(studentId,academicianId);
            return Ok(x);
        }
    }
}