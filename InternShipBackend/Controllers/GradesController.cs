using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Data;
using InternShipBackend.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private IAppRepository _appRepository;

        public GradesController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost]
        public IActionResult GradeReq([FromBody] GradeRequest req)
        {
            return Ok(_appRepository.SetGrade(req));
        }
        [HttpGet("IsGradeExist")]
        public IActionResult IsGradeExist(int id)
        {
            return Ok(_appRepository.isGradeExist(id));
        }

        [HttpGet("GetGrade")]
        public IActionResult GetGrade(int id)
        {
            return Ok(_appRepository.GetGradeResponse(id));
        }

    }
}