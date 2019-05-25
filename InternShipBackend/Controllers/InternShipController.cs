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
    public class InternShipController : ControllerBase
    {
        private IAppRepository _appRepository;

        public InternShipController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost]
        public ActionResult AddInternShip([FromBody] InternShipRequest req)
        {
            if (req != null)
            {
                var internShip = new InternShip()
                {
                    finishDate = req.finishDate,
                    startDate = req.startDate,
                    studentId = req.studentId
                };
                _appRepository.Add(internShip);
                _appRepository.SaveAll();
                _appRepository.createDays(req);
                return StatusCode(201);
            }

            return null;
        }
        [HttpGet("Assignment")]
        public ActionResult Assignment()
        {
            return StatusCode(201, _appRepository.assignment());

        }
        [HttpGet("GetAllAssignment")]
        public ActionResult GetAllAssignment()
        {
            return Ok(_appRepository.GetAllAssignment());

        }

        [HttpGet("IsThereInternShip")]
        public ActionResult GetInternShip(int id)
        {
            return Ok(_appRepository.isInternShipOK(id));

        }

        [HttpGet("getAssignment")]
        public ActionResult GetAssignment(int id)
        {
            return Ok(_appRepository.GetAssignmentForStudent(id));

        }
    }
}